
namespace LoreDrop.Services.Core.Contracts;
using Web.ViewModels.Series;

public interface ISeriesService
{
    Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync();
    
    Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(int? id, string? userId);
    
    Task<bool> CreateSeriesAsync(CreateSeriesFormViewModel model, string? userId);
}