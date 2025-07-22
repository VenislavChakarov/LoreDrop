using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public interface ISeriesStateRepository : IAsyncRepository<SeriesState, Guid>, IRepository<SeriesState, Guid>
{
    
}