using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class DetailsService : IDetailsService
{
    private readonly LoreDropDbContext _context;
    
    public DetailsService(LoreDropDbContext context)
    {
        _context = context;
    }
    
    public async Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(Guid? id, string? userId)
    {
        SeriesDetailesViewModel? detailsVm = null;
        if (id.HasValue)
        {
            Series? seriesModel = await _context.Series
                .Include(s => s.Genre)
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id.Value);

            if (seriesModel != null)
            {
                detailsVm = new SeriesDetailesViewModel
                {
                    Id = seriesModel.Id.ToString(),
                    Tittle = seriesModel.Tittle,
                    Author = seriesModel.Author,
                    Genre = seriesModel.Genre.Name,
                    Rating = seriesModel.Rating,
                    CreatedOn = seriesModel.CreatedOn,
                    ImageUrl = seriesModel.ImageUrl,
                    Description = seriesModel.Description
                };
            }
        }

        return detailsVm;
    }

    public async Task SetRatingAsync(Guid seriesId, double rating, string? userId)
    {
        if (rating < 1 || rating > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");
        }

        var series = await _context.Series.FindAsync(seriesId);
        if (series == null)
        {
            throw new KeyNotFoundException("Series not found.");
        }

        series.Rating = rating;
        await _context.SaveChangesAsync();
    }
}