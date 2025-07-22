using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public interface IGenreRepository 
    : IAsyncRepository<Genre, Guid>, IRepository<Genre, Guid>
{
    
}