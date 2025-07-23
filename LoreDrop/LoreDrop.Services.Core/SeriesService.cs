using System.Globalization;
using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.EntityFrameworkCore;
using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Services.Core;

public class SeriesService : ISeriesService
{
    private readonly ISeriesRepository seriesRepository;
    private readonly IGenreRepository genreRepository;
    
public SeriesService(ISeriesRepository context, IGenreRepository genreRepository)
{
        this.seriesRepository = context;
        this.genreRepository = genreRepository;
    }
    
    public async Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync()
    {
        var series = await seriesRepository.GetAllAttached()
            .Include(s => s.Ratings)
            .AsNoTracking()
            .Select(s => new AllSeriesIndexViewModel
            {
                Id = s.Id.ToString(),
                Title = s.Tittle,
                Author = s.Author,
                Genre = s.Genre.Name,
                Rating = s.Ratings.Any() ? (double?)s.Ratings.Average(r => r.Rating) : null,
                CreatedOn = s.CreatedOn.ToString(DateFormat),
                ImageUrl = s.ImageUrl
            })
            .ToListAsync();

        return series;
    }

    public async Task<bool> CreateSeriesAsync(CreateSeriesFormViewModel? model, string? userId)
    {
        bool optResult = false;
        
        Genre? genre = await genreRepository.GetByIdAsync(model.GenreId);
        
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

            await seriesRepository.AddAsync(series);
            
            optResult = true;
        }
        
        return optResult;
    }
}
