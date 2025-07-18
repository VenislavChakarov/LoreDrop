using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.WatchList;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class WatchListService : IWatchListService
{
    private readonly LoreDropDbContext _context;
    
    public WatchListService(LoreDropDbContext context)
    {
        _context = context;
    }
    
    public async  Task<IEnumerable<WatchListViewModel>> GetAllAsync(string userId)
    {
        var states = await _context.SeriesStates
            .Select(s => new AddSeriesStateDropDownMenu
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToListAsync();
    
        var watchList = await _context
            .UserWatchLists
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
        return _context.UserWatchLists
            .AnyAsync(us => us.UserId == userId && us.SeriesId.ToString() == seriesId);
    }

    public async Task<SeriesState?> GetSeriesStateAsync(string userId, string seriesId)
    {
        if (!Guid.TryParse(seriesId, out var seriesGuid))
        {
            throw new ArgumentException("Invalid series", nameof(seriesId));
        }

        var state = await  _context.UserWatchLists
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
            SeriesState = await _context.SeriesStates
                .FirstOrDefaultAsync(s => s.Name == "Ongoing") // Default state when adding to watchlist
        };
        
        await _context.UserWatchLists.AddAsync(UserWatchList);
        await _context.SaveChangesAsync();
    }

    public async Task ChageSateAsync(string seriesId, string userId, int stateId)
    {
        if (!Guid.TryParse(seriesId, out var seriesGuid))
        {
            throw new ArgumentException("Invalid series", nameof(seriesId));
        }
        
        var entry = await _context.UserWatchLists
            .FirstOrDefaultAsync(w =>
                w.UserId == userId &&
                w.SeriesId == seriesGuid);

        if (entry == null)
        {
            throw new InvalidOperationException("Watchlist entry not found.");
        }
        
        var stateExists = await _context.SeriesStates.AnyAsync(s => s.Id == stateId);
        if (!stateExists)
        {
            throw new ArgumentException("Invalid state", nameof(stateId));
        }
        
        entry.SeriesStateId = stateId;
        await _context.SaveChangesAsync();
    }
}