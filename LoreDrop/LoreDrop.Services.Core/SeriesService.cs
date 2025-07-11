using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.EntityFrameworkCore;
using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Services.Core;

public class SeriesService : ISeriesService
{
    private readonly LoreDropDbContext _context;
    
public SeriesService(LoreDropDbContext context)
{
        this._context = context;
    }
    
    public async Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync()
    {
        var series = await _context.Series
            .AsNoTracking()
            .Select(s => new AllSeriesIndexViewModel
            {
                Id = s.Id.ToString(),
                Title = s.Tittle,
                Author = s.Author,
                Genre = s.Genre.Name,
                Rating = s.Rating,
                CreatedOn = s.CreatedOn.ToString(DateFormat),
                ImageUrl = s.ImageUrl
            })
            .ToListAsync();

        return series;
    }

    public Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(int? id, string? userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateSeriesAsync(CreateSeriesFormViewModel model, string? userId)
    {
        throw new NotImplementedException();
    }
}
