using System.Globalization;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core.Admin.Interface;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Admin.SeriesAdmin;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.EntityFrameworkCore;
using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Services.Core.Admin;

public class SeriesAdminService : ISeriesAdminService
{
    private readonly ISeriesRepository seriesRepository;
    private readonly IGenreRepository genreRepository;
    private readonly IGenreService genreService;
    
    public SeriesAdminService(ISeriesRepository context, IGenreRepository genreRepository, IGenreService genreService)
    {
        this.seriesRepository = context;
        this.genreRepository = genreRepository;
        this.genreService = genreService;
    }
    
    public async Task<IEnumerable<AllSeriesIndexViewModel>> GetAllSeriesAsync()
    {
        var series = await seriesRepository.GetAllAttached()
            .IgnoreQueryFilters()
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
                ImageUrl = s.ImageUrl,
                IsDeleted = s.IsDeleted
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
                GenreId = genre.Id,
                CreatedOn = createdOn,
                ImageUrl = model.ImageUrl,
            };

            await seriesRepository.AddAsync(series);
            
            optResult = true;
        }
        
        return optResult;
    }

    public async Task<EditSerieViewModel?> GetSeriesForEditAsync(Guid seriesId, string? userId)
    {
        var series = await seriesRepository.GetAllAttached()
            .Select(s => new EditSerieViewModel
            {
                Id = s.Id.ToString(),
                Title = s.Tittle,
                ImageUrl = s.ImageUrl,
                Description = s.Description,
                Author = s.Author,
                CreatedOn = s.CreatedOn.ToString("yyyy-MM-dd"),
                GenreId = s.GenreId
            })
            .FirstOrDefaultAsync();

        if (series == null)
        {
            return null;
        }

        // Load genres
        series.Genres = await this.genreService.GetAllGenresAsync();

        return series;
    }

    public async Task<bool> EditSeriesAsync(EditSerieViewModel model, string? userId)
    {
        bool optResult = false;
        
        Genre? genre = await genreRepository.GetByIdAsync(model.GenreId);

        if (model != null && model.Id != null)
        {
            var series = await seriesRepository.GetByIdAsync(Guid.Parse(model.Id));

            if (series != null)
            {
                bool IsPublishedOnValid = DateTime.TryParseExact(model.CreatedOn, DateFormat, CultureInfo.InvariantCulture, 
                    DateTimeStyles.None, out DateTime createdOn);

                if (IsPublishedOnValid)
                {
                    series.Tittle = model.Title;
                    series.Description = model.Description;
                    series.Author = model.Author;
                    series.GenreId = genre.Id;
                    series.CreatedOn = createdOn;
                    series.ImageUrl = model.ImageUrl;

                    await seriesRepository.UpdateAsync(series);
                    
                    optResult = true;
                }
            }
        }

        return optResult;
    }

    public async Task<DeleteSeriesViewModel> GetSeriesForDeleteAsync(Guid seriesId, string? userId)
    {
        DeleteSeriesViewModel? deleteModel = null;

        if (seriesId != null)
        {
            var deleteSeriesModel = await seriesRepository.GetAllAttached()
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == seriesId);

            if (deleteSeriesModel != null)
            {
                deleteModel = new DeleteSeriesViewModel()
                {
                    Id = deleteSeriesModel.Id.ToString(),
                    Tittle = deleteSeriesModel.Tittle,
                    Author = deleteSeriesModel.Author,
                };
            }
            
        }

        return deleteModel;
    }

    public async Task<bool> SoftDeleteSeriesAsync(DeleteSeriesViewModel model, string userId)
    {
        bool optResult = false;

        if (model != null)
        {
            var series = await seriesRepository.GetAllAttached()
                .SingleOrDefaultAsync(s => s.Id == Guid.Parse(model.Id));

            if (series != null )
            {
                series.IsDeleted = true;

                await seriesRepository.SaveChangesAsync();
                
                optResult = true;
            }
        }

        return optResult;
    }
}