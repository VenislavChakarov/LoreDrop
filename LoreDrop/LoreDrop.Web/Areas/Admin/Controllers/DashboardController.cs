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
}