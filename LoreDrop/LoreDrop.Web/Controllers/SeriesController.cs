using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Authorization;
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

        public SeriesController(ISeriesService seriesService)
        {
            this.seriesService = seriesService;
        }

        [AllowAnonymous]
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
        
    }
}