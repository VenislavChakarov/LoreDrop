
namespace LoreDrop.Services.Core.Contracts;
using Web.ViewModels.Series;

public interface ISeriesService
{
    Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync();
}