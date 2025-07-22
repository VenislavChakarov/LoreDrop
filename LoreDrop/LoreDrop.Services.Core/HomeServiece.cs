using LoreDrop.Data;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Home;
using static LoreDrop.GCommon.ValidationConstants.Series;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class HomeServiece : IHomeService
{
    private readonly SeriesRepsitory seriesRepsitory;

    public HomeServiece(SeriesRepsitory context)
    {
        seriesRepsitory = context;
    }

    public async Task<IEnumerable<TopRatedSeries>> GetTopRatedSeriesAsync()
    {
        var top3 = await seriesRepsitory.GetAllAttached()
            .Include(s => s.Genre)
            .Include(s => s.Ratings)
            .OrderByDescending(s => s.Ratings.Any() ? s.Ratings.Average(r => r.Rating) : 0)
            .Take(3)
            .Select(s => new TopRatedSeries
            {
                Id = s.Id.ToString(),
                Tittle = s.Tittle,
                Rating = s.Ratings.Any() ? (double?)s.Ratings.Average(r => r.Rating) : null,
                Author = s.Author,
                Genre = s.Genre.Name,
                CreatedOn = s.CreatedOn.ToString(DateFormat),
                ImageUrl = s.ImageUrl
            })
            .ToListAsync();

        return top3;
    }
}