using LoreDrop.Web.ViewModels.Series;

namespace LoreDrop.Services.Core.Contracts;

public interface IDetailsService
{
    Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(int? id, string? userId);
    Task SetRatingAsync(int seriesId, double rating, string? userId);
}