using System.Globalization;
using LoreDrop.Data;
using LoreDrop.Data.Models;
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

    public async Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(int? id, string? userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateSeriesAsync(CreateSeriesFormViewModel? model, string? userId)
    {
        bool optResult = false;
        
        Genre? genre = await _context.Genres.FindAsync(model.GenreId);
        
        bool IsPublishedOnValid = DateTime.TryParseExact(model.CreatedOn, DateFormat, CultureInfo.InvariantCulture, 
            DateTimeStyles.None, out DateTime createdOn);
        
        if (genre != null && IsPublishedOnValid)
        {
            Series series = new Series
            {
                Tittle = model.Title,
                Description = model.Description,
                Author = model.Author,
                GenreId = model.GenreId,
                CreatedOn = createdOn,
                ImageUrl = model.ImageUrl,
            };

            await _context.Series.AddAsync(series);
            int result = await _context.SaveChangesAsync();
            
            optResult = result > 0;
        }
        
        return optResult;
    }
}
