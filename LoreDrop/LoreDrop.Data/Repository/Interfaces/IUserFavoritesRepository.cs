using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public interface IUserFavoritesRepository : IAsyncRepository<UserFavorites, Guid>, IRepository<UserFavorites, Guid>
{
    
}