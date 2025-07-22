using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Favorites;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class FavoriteService : IFavoriteService
{
    private readonly UserFavoriteRepository favRepository;
    public FavoriteService(UserFavoriteRepository context)
    {
        favRepository = context;
    }
    
    public async  Task<IEnumerable<FavoriteSereisViewModel>> GetUserFavoritesAsync(string userId)
    {
        var favorites = await favRepository.GetAllAttached()
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

    public async Task<bool> IsSeriesInFavoritesAsync(string userId, string seriesId)
    {
        return await favRepository.GetAllAttached()
            .AnyAsync(us => us.UserId == userId && us.SeriesId.ToString() == seriesId);
    }

    public async Task AddToFavoritesAsync(string userId, string seriesId)
    {
        var userFavorite = new UserFavorites()
        {
            UserId = userId,
            SeriesId = Guid.Parse(seriesId)
        };

        await favRepository.AddAsync(userFavorite);
    }

    public async Task RemoveFromFavoritesAsync (string userId, string seriesId)
    {
        var userFavorite = favRepository
            .FirstOrDefaultAsync(us => us.UserId == userId && us.SeriesId.ToString() == seriesId);

        if (userFavorite != null)
        {
            await favRepository.HardDeleteAsync(userFavorite.Result);
            await favRepository.SaveChangesAsync();
        }
    }
}