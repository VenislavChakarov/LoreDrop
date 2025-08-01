using LoreDrop.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LoreDrop.Controllers;

public class WatchListController : BaseController
{
    private readonly IWatchListService watchListService;
    
    public WatchListController(IWatchListService watchListService)
    {
        this.watchListService = watchListService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        
        var userId = this.GetUserId();
        var watchList = await this.watchListService.GetAllAsync(userId);
        
        return View(watchList);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddToWatchList(string seriesId)
    {
        try
        {
            if (string.IsNullOrEmpty(seriesId))
            {
                ModelState.AddModelError("seriesId", "Try again, something went wrong.");
                return RedirectToAction("Details", "Details", new { id = seriesId });;
            }

            var userId = this.GetUserId();
            bool isInWatchList = await this.watchListService.IsSeriesInWatchListAsync(userId, seriesId);

            if (!isInWatchList)
            {
                await this.watchListService.AddToWatchListAsync(userId, seriesId);
            }
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ModelState.AddModelError("seriesId", "Try again, something went wrong.");
            return RedirectToAction("Details", "Details", new { id = seriesId });
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromWatchList(string seriesId)
    {
        try
        {
            if (string.IsNullOrEmpty(seriesId))
            {
                ModelState.AddModelError("seriesId", "Try again, something went wrong.");
                return RedirectToAction("Index");
            }
            
            var userId = this.GetUserId();
            bool isInWatchList = await this.watchListService.IsSeriesInWatchListAsync(userId, seriesId);
            
            if (isInWatchList)
            {
                await this.watchListService.RemoveFromWatchListAsync(userId, seriesId);
            }
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ModelState.AddModelError("seriesId", "Try again, something went wrong.");
            return RedirectToAction("Index");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> ChangeState(string seriesId, Guid stateId)
    {
        try
        {
            if (string.IsNullOrEmpty(seriesId))
            {
                ModelState.AddModelError("seriesId", "Try again, something went wrong.");
                return RedirectToAction("Index");
            }
            
            var userId = this.GetUserId();
            
            bool isInWatchList = await this.watchListService.IsSeriesInWatchListAsync(userId, seriesId);

            if (!isInWatchList)
            {
                ModelState.AddModelError("seriesId", "Series is not in your watchlist.");
            }
            
            await this.watchListService.ChageSateAsync(seriesId, userId, stateId);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ModelState.AddModelError("seriesId", "Try again, something went wrong.");
            return RedirectToAction("Index");
        }
    }
    
}