using LoreDrop.Data;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class GenreService : IGenreService
{
    private readonly LoreDropDbContext _context;
    
    public GenreService(LoreDropDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<AddSeriesGenreDropDownMenu>> GetAllGenresAsync()
    {
        var gernesDropDownMenu = await _context
            .Genres
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