using LoreDrop.Web.ViewModels.Series;

namespace LoreDrop.Services.Core.Contracts;

public interface IDetailsService
{
    Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(Guid? id);
    Task SetRatingAsync(Guid seriesId, double rating, string? userId);
}