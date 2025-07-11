using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Controllers
{
    public class SeriesController : BaseController
    {

        private readonly ISeriesService seriesService;
        private readonly IGenreService genreService;

        public SeriesController(ISeriesService seriesService, IGenreService genreService)
        {
            this.seriesService = seriesService;
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var allSeries = await this.seriesService
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

                bool isCreated = await this.seriesService.CreateSeriesAsync(inputModel, this.GetUserId());

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
}