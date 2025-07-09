using Microsoft.AspNetCore.Identity;

namespace LoreDrop.Data.Models;

public class Series
{
    public int Id { get; set; }

    public string Tittle { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public string Author { get; set; } = null!;
    
    public double? Rating { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public virtual DateTime CreatedOn { get; set; }
    
    public int GenreId { get; set; }
    
    public virtual Genre Genre { get; set; } = null!;   
    
    public bool IsDeleted { get; set; }
    
    public virtual ICollection<UserFavorites> UserFavorites { get; set; } = new HashSet<UserFavorites>();
    
    public virtual ICollection<UserSaved> UserSaved { get; set; } = new HashSet<UserSaved>();

    public virtual ICollection<Comments> Comments { get; set; } = new HashSet<Comments>();
}