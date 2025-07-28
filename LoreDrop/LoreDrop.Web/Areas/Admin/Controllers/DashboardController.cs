using LoreDrop.Services.Core.Admin.Interface;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Admin.SeriesAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : BaseAdminController
{
    private readonly ISeriesAdminService seriesAdminService;
    private readonly IGenreService genreService;
    
    public DashboardController(ISeriesAdminService seriesAdminService, IGenreService genreService)
    {
        this.seriesAdminService = seriesAdminService;
        this.genreService = genreService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var allSeries = await this.seriesAdminService
                .GetAllSeriesAsync();

            return View(allSeries);


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index), "Home");
        }


    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            CreateSeriesFormViewModel inputModel = new CreateSeriesFormViewModel()
            {
                CreatedOn = DateTime.UtcNow.ToString(DateFormat),
                Genres = await this.genreService.GetAllGenresAsync()

            };
            return View(inputModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSeriesFormViewModel inputModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                inputModel.Genres = await this.genreService.GetAllGenresAsync();
                return View(inputModel);
            }

            bool isCreated = await this.seriesAdminService.CreateSeriesAsync(inputModel, this.GetUserId());

            if (!isCreated)
            {
                ModelState.AddModelError(string.Empty,
                    "There was an error while creating the series. Please try again.");
                inputModel.Genres = await this.genreService.GetAllGenresAsync();
            }
                
            return RedirectToAction(nameof(Index));
                
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            inputModel.Genres = await this.genreService.GetAllGenresAsync();
            return View(inputModel);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid seriesId)
        {
            try
            {
                var userId = this.GetUserId();
                var model = await this.seriesAdminService.GetSeriesForEditAsync(seriesId, userId);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentException("An error occurred while retrieving the series for editing.", e);
            }
        }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditSerieViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // Redisplay the form with validation errors
                return View(model);
            }

            // Pass in the current user’s ID if you track LastModifiedBy, etc.
            string userId = User.FindFirst("sub")?.Value ?? User.Identity.Name;
            var success = await this.seriesAdminService.EditSeriesAsync(model, userId);

            if (!success)
            {
                // handle failure (e.g. show a generic error)
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View(model); 
        }
        
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(DeleteSeriesViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // Optionally add message
                TempData["Error"] = "Invalid data.";
                return RedirectToAction(nameof(Index));
            }

            var userId = this.GetUserId();
            var success = await this.seriesAdminService.SoftDeleteSeriesAsync(model, userId);

            if (!success)
            {
                TempData["Error"] = "Unable to delete series. Please try again.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Series removed successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "An error occurred while deleting the series. Please try again.");
            return View(nameof(Index)); 
        }
        
    }
    
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Restore(RestoreSeriesViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data.";
                return RedirectToAction(nameof(Index));
            }

            var userId = this.GetUserId();
            var success = await this.seriesAdminService.RestoreSeriesAsync(model, userId);

            if (!success)
            {
                TempData["Error"] = "Unable to restore series. Please try again.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Series restored successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "An error occurred while restoring the series. Please try again.");
            return View(nameof(Index)); 
        }
        
    }
    
    [HttpGet]
    public async Task<IActionResult> HardDelete(Guid seriesId)
    {
        try
        {
            var id = seriesId;
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            
            var userId = this.GetUserId();
            var model = await this.seriesAdminService.GetSeriesForHardDeleteAsync(seriesId, userId);
                
            if (model == null)
            {
                    return NotFound();
            }
    
            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index));
        }
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> HardDelete(HardDeleteSeriesViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // If the model isn’t valid, re‑show the confirmation page
                return RedirectToAction(nameof(Index));
            }

            // Parse the GUID
            if (!Guid.TryParse(model.Id, out var seriesId))
            {
                ModelState.AddModelError("", "Invalid series identifier.");
                return View("HardDelete", model);
            }

            // Call the service to hard‑delete
            var userId = this.GetUserId();
            var success = await seriesAdminService.HardDeleteSeriesAsync(seriesId, userId);

            if (!success)
            {
                ModelState.AddModelError("", "Unable to delete series permanently. Please try again.");
                return View("HardDelete", model);
            }

            // Optionally add a success message
            TempData["Success"] = "Series deleted permanently.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ModelState.AddModelError("", "An unexpected error occurred.");
            return View("HardDelete", model);
        }
    }

}

