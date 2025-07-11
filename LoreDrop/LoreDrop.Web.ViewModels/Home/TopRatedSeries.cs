namespace LoreDrop.Web.ViewModels.Home;

public class TopRatedSeries
{
    public string Id { get; set; } = null!;
    
    public string Tittle { get; set; } = null!;
    
    public string Genre { get; set; } = null!;
    
    public string Author { get; set; } = null!;
    
    public string CreatedOn { get; set; } = null!;
    
    public double? Rating { get; set; }
    
    public string? ImageUrl { get; set; }
}