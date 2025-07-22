using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public interface IUserWatchListRepository : IAsyncRepository<UserWatchList, Guid>, IRepository<UserWatchList, Guid>
{
    
}