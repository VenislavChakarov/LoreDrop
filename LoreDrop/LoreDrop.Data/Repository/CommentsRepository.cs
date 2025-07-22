using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;

namespace LoreDrop.Data.Repository;

public class CommentsRepository : BaseRepository<Comments, Guid>, ICommentsRepository
{
    public CommentsRepository(LoreDropDbContext context) 
        : base(context)
    {
    }
}