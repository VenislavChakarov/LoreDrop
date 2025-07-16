namespace LoreDrop.Web.ViewModels.Series;

public class SeriesDetailesViewModel
{
    public string Id { get; set; } = null!;
    
    public string Tittle { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public string Genre { get; set; } = null!;
    
    public string Author { get; set; } = null!;
    
    public double? Rating { get; set; }
    
    public double? AverageRating { get; set; } 
    public double? UserRating { get; set; } 
    
    public string? ImageUrl { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public bool IsFavorite { get; set; }
    
    public bool IsUserWatchList { get; set; }
    
    public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
       // The current user's rating for this series, if any
}