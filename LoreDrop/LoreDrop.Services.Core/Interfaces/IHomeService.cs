using LoreDrop.Web.ViewModels.Home;

namespace LoreDrop.Services.Core.Contracts;

public interface IHomeService
{
    Task<IEnumerable<TopRatedSeries>> GetTopRatedSeriesAsync();
}