using Microsoft.AspNetCore.Identity;

namespace LoreDrop.Data.Models;

public class Content
{
    public int Id { get; set; }

    public string Tittle { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public double Rating { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public int GenreId { get; set; }
    
    public virtual Genre Genre { get; set; } = null!;   
    
    public bool IsDeleted { get; set; }
    
    public ICollection<UserFavorites> UserFavorites { get; set; } = new HashSet<UserFavorites>();
    
    public ICollection<UserSaved> UserSaved { get; set; } = new HashSet<UserSaved>();
    
}