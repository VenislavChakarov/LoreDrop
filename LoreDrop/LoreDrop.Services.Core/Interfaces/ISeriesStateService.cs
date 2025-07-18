using LoreDrop.Web.ViewModels.WatchList;

namespace LoreDrop.Services.Core.Contracts;

public interface ISeriesStateService
{
    Task<IEnumerable<AddSeriesStateDropDownMenu>> GetAllStatesAsync();
}