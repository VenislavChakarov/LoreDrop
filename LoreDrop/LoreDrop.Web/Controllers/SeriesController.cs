using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LoreDrop.Data;
using LoreDrop.Web.ViewModels.Series;

namespace LoreDrop.Controllers
{
    public class SeriesController : BaseController
    {
        private readonly LoreDropDbContext _context;

        public SeriesController(LoreDropDbContext context)
        {
            _context = context;
        }

        // GET: /Series/
        public IActionResult Index()
        {
            // you can add pagination here later
            var allSeries = _context.Series
                .OrderByDescending(s => s.Rating)
                .ToList();
            
            return View(allSeries);
        }

        // GET: /Series/Details/5
        public IActionResult Details(int id)
        {
            var series = _context.Series.Find(id);
            if (series == null) return NotFound();
            return View(series);
        }
    }
}