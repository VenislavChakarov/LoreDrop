using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LoreDrop.Controllers
{
    public class SeriesController : BaseController
    {
        
        private readonly ISeriesService seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            this.seriesService = seriesService;
        }

        // GET: /Series/
        public async Task<IActionResult> Index()
        {
            // you can add pagination here later
            var allSeries = await this.seriesService
                .GetAllSeriesAsync();
            
            return View(allSeries);
        }

        // GET: /Series/Details/5
        
        
        public IActionResult Create()
        {
            return View(new CreateSeriesFormViewModel());
        }
    }
}