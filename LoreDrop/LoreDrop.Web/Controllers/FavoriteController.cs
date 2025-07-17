using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Favorites;
using Microsoft.AspNetCore.Mvc;
using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class FavoriteController : BaseController
{
    private readonly IFavoriteService favoritesService;
    
    public FavoriteController(IFavoriteService favoritesService)
    {
        this.favoritesService = favoritesService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var userId = this.GetUserId();
            var favoriteSeries = await this.favoritesService.GetUserFavoritesAsync(userId);
            
            var model =  favoriteSeries
                .Select(fs => new FavoriteSereisViewModel
                {
                    SeriesId = fs.SeriesId,
                    Title = fs.Title,
                    ImageUrl = fs.ImageUrl,
                    Author = fs.Author,
                    Genre = fs.Genre,
                    CreatedOn = fs.CreatedOn,
                }).ToList();
            
            return View(model);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToFavorites(Guid seriesId)
    {
        try
        {
            if (seriesId == Guid.Empty)
            {
                ModelState.AddModelError("seriesId", "Try again later");
                return RedirectToAction("Details", "Details", new { id = seriesId }); // or handle accordingly
            }
            
            var userId = this.GetUserId();
            bool isFavorite = await this.favoritesService.IsSeriesInFavoritesAsync(userId, seriesId.ToString());
            if (!isFavorite)
            {
                await this.favoritesService.AddToFavoritesAsync(userId, seriesId.ToString());
            }
            
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Details", "Details", new { id = seriesId });
        }
    }
}