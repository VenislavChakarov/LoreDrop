using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;

namespace LoreDrop.Data.Repository;

public class UserWatchListRepository : BaseRepository<UserWatchList, Guid>, IUserWatchListRepository
{
    public UserWatchListRepository(LoreDropDbContext context) : base(context)
    {
    }
}