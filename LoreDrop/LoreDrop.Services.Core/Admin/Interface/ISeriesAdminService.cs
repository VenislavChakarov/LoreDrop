using LoreDrop.Web.ViewModels.Admin.SeriesAdmin;
using LoreDrop.Web.ViewModels.Series;


namespace LoreDrop.Services.Core.Admin.Interface;

public interface ISeriesAdminService
{
    Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync();
    Task<bool> CreateSeriesAsync(CreateSeriesFormViewModel model, string? userId);
    
    Task<EditSerieViewModel?> GetSeriesForEditAsync(Guid seriesId, string? userId);
    Task<bool> EditSeriesAsync(EditSerieViewModel model, string? userId);
    
    Task<bool> SoftDeleteSeriesAsync(DeleteSeriesViewModel model, string userId);
    
    Task<bool> RestoreSeriesAsync(RestoreSeriesViewModel model, string userId);
    
    Task<DeleteSeriesViewModel> GetSeriesForHardDeleteAsync(Guid seriesId, string? userId);
    
    Task<bool> HardDeleteSeriesAsync(Guid seriesId, string userId);
}