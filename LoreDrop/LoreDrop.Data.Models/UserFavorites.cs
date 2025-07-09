using Microsoft.AspNetCore.Identity;

namespace LoreDrop.Data.Models;

public class UserFavorites
{
    public string UserId { get; set; } = null!;
    
    public virtual IdentityUser User { get; set; } = null!;
    
    public int SeriesId { get; set; }
    
    public virtual Series Series { get; set; } = null!;
}