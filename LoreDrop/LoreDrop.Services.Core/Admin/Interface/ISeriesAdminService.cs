using LoreDrop.Web.ViewModels.Admin.SeriesAdmin;
using LoreDrop.Web.ViewModels.Series;


namespace LoreDrop.Services.Core.Admin.Interface;

public interface ISeriesAdminService
{
    Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync();
    Task<bool> CreateSeriesAsync(CreateSeriesFormViewModel model, string? userId);
}