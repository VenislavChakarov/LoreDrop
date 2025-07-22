using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public interface ISeriesRepository : IAsyncRepository<Series, Guid>, IRepository<Series, Guid>
{
    
}