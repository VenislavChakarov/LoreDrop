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
    
    public SeriesService(ISeriesRepository context)
    {
        this.seriesRepository = context;
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
    
}
