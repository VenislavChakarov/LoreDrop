using LoreDrop.Data.Models;

namespace LoreDrop.Data.Repository.Interfaces;

public interface ICommentsRepository 
    : IAsyncRepository<Comments, Guid>, IRepository<Comments, Guid>
{
    
}