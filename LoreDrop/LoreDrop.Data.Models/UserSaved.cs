using Microsoft.AspNetCore.Identity;

namespace LoreDrop.Data.Models;

public class UserSaved
{
    public string UserId { get; set; } = null!;
    
    public virtual IdentityUser User { get; set; } = null!;
    
    public int ContentId { get; set; }
    
    public virtual Content Content { get; set; } = null!;
}