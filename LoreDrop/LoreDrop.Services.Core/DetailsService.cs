using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class DetailsService : IDetailsService
{
    private readonly LoreDropDbContext _context;
    private readonly IFavoriteService _favoritesService;
    
    public DetailsService(LoreDropDbContext context, IFavoriteService favoritesService)
    {
        _favoritesService = favoritesService;
        _context = context;
    }
    
    public async Task<SeriesDetailesViewModel> GetSeriesDetailsAsync(Guid? id, string? userId)
    {
        if (id == null) return null;

        var series = await _context.Series
            .Include(s => s.Genre)
            .Include(s => s.Ratings)
            .Include(s => s.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (series == null) return null;
        
        double? averageRating = series.Ratings.Any() ? (double?)series.Ratings.Average(x => x.Rating) : null;
        
        double? userRating = null;
        if (!string.IsNullOrWhiteSpace(userId))
        {
            var userRatingEntity = series.Ratings.FirstOrDefault(r => r.UserId == userId);
            if (userRatingEntity != null)
                userRating = userRatingEntity.Rating;
        }
        
        bool isFavorite = false;
        if (!string.IsNullOrWhiteSpace(userId))
        {
            isFavorite = await _favoritesService
                .IsSeriesInFavoritesAsync(userId, series.Id.ToString());
        }

        var viewModel = new SeriesDetailesViewModel
        {
            Id = series.Id.ToString(),
            Tittle = series.Tittle,
            Description = series.Description,
            Genre = series.Genre?.Name,
            Author = series.Author,
            ImageUrl = series.ImageUrl,
            CreatedOn = series.CreatedOn,
            AverageRating = averageRating,
            UserRating = userRating,
            IsFavorite = isFavorite,
            
        };
        return viewModel;
    }

    public async Task SetRatingAsync(Guid seriesId, double rating, string? userId)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        var existing = await _context.SeriesRatings
            .FirstOrDefaultAsync(s => s.SeriesId == seriesId && s.UserId == userId);
        if (existing != null)
        {
            existing.Rating = rating;
        }
        else
        {
            _context.SeriesRatings.Add(new SeriesRating
            {
                SeriesId = seriesId,
                UserId = userId,
                Rating = rating,
            });
        }
        await _context.SaveChangesAsync();
    }
}