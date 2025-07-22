namespace LoreDrop.Data.Repository;

public class SeriesStateRepository : BaseRepository<Models.SeriesState, Guid>, Interfaces.ISeriesStateRepository
{
    public SeriesStateRepository(LoreDropDbContext context) 
        : base(context)
    {
    }
}