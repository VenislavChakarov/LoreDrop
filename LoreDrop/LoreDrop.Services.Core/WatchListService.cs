using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.WatchList;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class WatchListService : IWatchListService
{
    private readonly UserWatchListRepository watchListRepository;
    private readonly SeriesStateRepository stateRepository;
    
    public WatchListService(UserWatchListRepository context, SeriesStateRepository stateRepository)
    {
        watchListRepository = context;
        this.stateRepository = stateRepository;
    }
    
    public async  Task<IEnumerable<WatchListViewModel>> GetAllAsync(string userId)
    {
        var states = await stateRepository.GetAllAttached()
            .Select(s => new AddSeriesStateDropDownMenu
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToListAsync();
    
        var watchList = await watchListRepository.GetAllAttached()
            .Where(w => w.UserId == userId)
            .Select(us => new WatchListViewModel
            {
                SeriesId = us.SeriesId.ToString(),
                Title = us.Series.Tittle,
                ImageUrl = us.Series.ImageUrl,
                StateId = us.SeriesState.Id,
                StateDropDownMenu = states
            })
            .ToListAsync();

        return watchList;
    }

    public Task<bool> IsSeriesInWatchListAsync(string userId, string seriesId)
    {
        return watchListRepository.GetAllAttached()
            .AnyAsync(us => us.UserId == userId && us.SeriesId.ToString() == seriesId);
    }

    public async Task<SeriesState?> GetSeriesStateAsync(string userId, string seriesId)
    {
        if (!Guid.TryParse(seriesId, out var seriesGuid))
        {
            throw new ArgumentException("Invalid series", nameof(seriesId));
        }

        var state = await watchListRepository.GetAllAttached()
            .Where(us => us.UserId == userId && us.SeriesId == seriesGuid)
            .Select(us => us.SeriesState)
            .FirstOrDefaultAsync();
        
        return state;
    }

    public async Task AddToWatchListAsync(string userId, string seriesId)
    {
        var UserWatchList = new UserWatchList()
        {
            UserId = userId,
            SeriesId = Guid.Parse(seriesId),
            SeriesState = await stateRepository
                .FirstOrDefaultAsync(s => s.Name == "Ongoing") // Default state when adding to watchlist
        };
        
        await watchListRepository.AddAsync(UserWatchList);
    }

    public async Task ChageSateAsync(string seriesId, string userId, Guid stateId)
    {
        if (!Guid.TryParse(seriesId, out var seriesGuid))
        {
            throw new ArgumentException("Invalid series", nameof(seriesId));
        }
        
        var entry = await watchListRepository
            .FirstOrDefaultAsync(w =>
                w.UserId == userId &&
                w.SeriesId == seriesGuid);

        if (entry == null)
        {
            throw new InvalidOperationException("Watchlist entry not found.");
        }
        
        var stateExists = await stateRepository.GetAllAttached()
            .AnyAsync(s => s.Id == stateId);
        if (!stateExists)
        {
            throw new ArgumentException("Invalid state", nameof(stateId));
        }
        
        entry.SeriesStateId = stateId;
        await watchListRepository.SaveChangesAsync();
    }
}