using LoreDrop.Data;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.WatchList;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class SeriesStateService : ISeriesStateService
{
    private readonly SeriesStateRepository stateRepository;
    
public SeriesStateService(SeriesStateRepository context)
    {
        stateRepository = context;
    }
    
    public async Task<IEnumerable<AddSeriesStateDropDownMenu>> GetAllStatesAsync()
    {
        var seriesStatesDropDownMenu = await stateRepository.GetAllAttached()
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