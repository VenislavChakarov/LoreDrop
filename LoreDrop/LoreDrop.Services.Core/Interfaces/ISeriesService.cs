
namespace LoreDrop.Services.Core.Contracts;
using Web.ViewModels.Series;

public interface ISeriesService
{
    Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync();
    Task<bool> CreateSeriesAsync(CreateSeriesFormViewModel model, string? userId);
}