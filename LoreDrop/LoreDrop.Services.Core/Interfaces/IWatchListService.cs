using LoreDrop.Data.Models;
using LoreDrop.Web.ViewModels.WatchList;

namespace LoreDrop.Services.Core.Contracts;

public interface IWatchListService
{
    Task<IEnumerable<WatchListViewModel>> GetAllAsync(string userId);

    Task<bool> IsSeriesInWatchListAsync(string userId, string seriesId);
    
    Task<SeriesState?> GetSeriesStateAsync(string userId, string seriesId);

    Task AddToWatchListAsync(string userId, string seriesId);

    Task ChageSateAsync(string seriesId, string userId, int stateId);
}