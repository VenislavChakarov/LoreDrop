using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public class SeriesRatingRepository : BaseRepository<SeriesRating, Guid>, ISeriesRatingRepository
{
    public SeriesRatingRepository(LoreDropDbContext context) : base(context)
    {
    }
}