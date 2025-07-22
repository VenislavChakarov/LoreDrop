using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;

namespace LoreDrop.Data.Repository;

public class SeriesRepsitory : BaseRepository<Series, Guid>, ISeriesRepository
{
    public SeriesRepsitory(LoreDropDbContext dbContext) 
        : base(dbContext)
    {
        
    }
}