using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;

namespace LoreDrop.Data.Repository;

public class SeriesRepository : BaseRepository<Series, Guid>, ISeriesRepository
{
    public SeriesRepository(LoreDropDbContext dbContext) 
        : base(dbContext)
    {
        
    }
}