namespace LoreDrop.Web.ViewModels.Favorites;

public class FavoriteSereisViewModel
{
    public string SeriesId { get; set; } = null!;
    public string Title { get; set; } = null!;
    
    public string Genre { get; set; } = null!;
    
    public string Author { get; set; } = null!;
    
    public string CreatedOn { get; set; } = null!;
    public string? ImageUrl { get; set; }
}