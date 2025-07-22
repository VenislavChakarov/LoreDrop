using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;

namespace LoreDrop.Data.Repository;

public class GenreRepository : BaseRepository<Genre, Guid>, IGenreRepository
{
    public GenreRepository(LoreDropDbContext context) 
        : base(context)
    {
        
    }
}