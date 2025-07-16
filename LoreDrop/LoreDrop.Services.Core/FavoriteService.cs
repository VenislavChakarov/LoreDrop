using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Favorites;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class FavoriteService : IFavoriteService
{
    private readonly LoreDropDbContext context;
    public FavoriteService(LoreDropDbContext context)
    {
        context = context;
    }
    
    public async  Task<IEnumerable<FavoriteSereisViewModel>> GetUserFavoritesAsync(string userId)
    {
        var favorites = await context.UserFavorites
            .Where(us => us.UserId == userId)
            .Select(us => new FavoriteSereisViewModel
            {
                SeriesId = us.SeriesId.ToString(),
                Title = us.Series.Tittle,
                ImageUrl = us.Series.ImageUrl,
                Author = us.Series.Author,
                Genre = us.Series.Genre.Name,
                CreatedOn = us.Series.CreatedOn.ToString("yyyy-MM-dd")
            })
            .ToListAsync();
        
        return favorites;
    }

    public Task<bool> IsSeriesInFavoritesAsync(string userId, string seriesId)
    {
        throw new NotImplementedException();
    }

    public Task AddToFavoritesAsync(string userId, string seriesId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromFavoritesAsync(string userId, string seriesId)
    {
        throw new NotImplementedException();
    }
}