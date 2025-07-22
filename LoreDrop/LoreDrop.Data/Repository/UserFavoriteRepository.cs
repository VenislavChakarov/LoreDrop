using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;

namespace LoreDrop.Data.Repository;

public class UserFavoriteRepository : BaseRepository<UserFavorites, Guid>, IUserFavoritesRepository
{
    public UserFavoriteRepository(LoreDropDbContext context) : base(context)
    {
    }
}