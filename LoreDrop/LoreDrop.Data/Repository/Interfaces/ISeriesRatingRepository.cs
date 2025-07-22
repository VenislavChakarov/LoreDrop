using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public interface ISeriesRatingRepository : IAsyncRepository<SeriesRating, Guid>, IRepository<SeriesRating, Guid>
{
    
}