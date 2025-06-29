using Microsoft.AspNetCore.Identity;

namespace LoreDrop.Data.Models;

public class Comments
{
    public int Id { get; set; }
    
    public int? ContentId { get; set; }
    
    public string UserId { get; set; } = null!;
    
    public string Text { get; set; } = null!;
    
    public virtual DateTime CreatedOn { get; set; }

    public virtual Content? Content { get; set; } = null!;
    
    public virtual IdentityUser User { get; set; } = null!;
}