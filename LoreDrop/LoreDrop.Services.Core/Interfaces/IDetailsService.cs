using LoreDrop.Web.ViewModels.Series;

namespace LoreDrop.Services.Core.Contracts;

public interface IDetailsService
{
    Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(Guid? id, string? userId);
    Task SetRatingAsync(Guid seriesId, double rating, string? userId);
}