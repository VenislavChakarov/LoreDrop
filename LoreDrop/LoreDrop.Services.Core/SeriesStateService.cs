using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.WatchList;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class SeriesStateService : ISeriesStateService
{
    private readonly LoreDropDbContext _context;
    
public SeriesStateService(LoreDropDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<AddSeriesStateDropDownMenu>> GetAllStatesAsync()
    {
        var seriesStatesDropDownMenu = await _context
            .SeriesStates
            .AsNoTracking()
            .Select(s => new AddSeriesStateDropDownMenu
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToListAsync();
        
        return seriesStatesDropDownMenu;
    }
}