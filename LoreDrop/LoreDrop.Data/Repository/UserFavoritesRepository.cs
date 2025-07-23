using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;

namespace LoreDrop.Data.Repository;

public class UserFavoritesRepository : BaseRepository<UserFavorites, Guid>, IUserFavoritesRepository
{
    public UserFavoritesRepository(LoreDropDbContext context) : base(context)
    {
    }
}