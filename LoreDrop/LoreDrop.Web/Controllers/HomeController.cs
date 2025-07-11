using System.Diagnostics;
using LoreDrop.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LoreDrop.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeService homeService;

    public HomeController(ILogger<HomeController> logger, IHomeService homeService)
    {
        this.homeService = homeService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var topSeries = await this.homeService.GetTopRatedSeriesAsync();
        
        return View(topSeries);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return NotFound();
        // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
