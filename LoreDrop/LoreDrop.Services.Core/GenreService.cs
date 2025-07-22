using LoreDrop.Data;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class GenreService : IGenreService
{
    private readonly GenreRepository genreRepository;
    
    public GenreService(GenreRepository context)
    {
        genreRepository = context;
    }
    
    public async Task<IEnumerable<AddSeriesGenreDropDownMenu>> GetAllGenresAsync()
    {
        var gernesDropDownMenu = await genreRepository.GetAllAttached()
            .AsNoTracking()
            .Select(g => new AddSeriesGenreDropDownMenu
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToListAsync();
        
        return gernesDropDownMenu;
    }
}