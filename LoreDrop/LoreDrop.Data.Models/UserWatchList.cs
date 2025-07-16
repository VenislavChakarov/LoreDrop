using Microsoft.AspNetCore.Identity;

namespace LoreDrop.Data.Models;

public class UserWatchList
{
    public string UserId { get; set; } = null!;
    
    public virtual IdentityUser User { get; set; } = null!;
    
    public Guid SeriesId { get; set; }
    
    public virtual Series Series { get; set; } = null!;
    
    public int SeriesStateId { get; set; }
    
    public virtual SeriesState SeriesState { get; set; } = null!;

}