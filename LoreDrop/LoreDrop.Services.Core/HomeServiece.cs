using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Home;
using static LoreDrop.GCommon.ValidationConstants.Series;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class HomeServiece : IHomeService
{
    private readonly LoreDropDbContext _context;

    public HomeServiece(LoreDropDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TopRatedSeries>> GetTopRatedSeriesAsync()
    {
        return await _context.Series
            .OrderByDescending(s => s.Rating)
            .Take(3)
            .Select(s => new TopRatedSeries
            {
                Id = s.Id.ToString(),
                Tittle = s.Tittle,
                Rating = s.Rating,
                Author = s.Author,
                Genre = s.Genre.Name,
                CreatedOn = s.CreatedOn.ToString(DateFormat),
                ImageUrl = s.ImageUrl
            })
            .ToListAsync();
    }
}