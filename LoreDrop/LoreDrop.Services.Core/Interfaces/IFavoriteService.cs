using LoreDrop.Web.ViewModels.Favorites;

namespace LoreDrop.Services.Core.Contracts;

public interface IFavoriteService
{
    Task<IEnumerable<FavoriteSereisViewModel>> GetUserFavoritesAsync(string userId);
    
    Task<bool> IsSeriesInFavoritesAsync(string userId, string seriesId);
    
    Task AddToFavoritesAsync(string userId, string seriesId);
    
    Task RemoveFromFavoritesAsync(string userId, string seriesId);
}