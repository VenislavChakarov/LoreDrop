namespace LoreDrop.Web.ViewModels.Series;

public class AllSeriesIndexViewModel
{
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public double? Rating { get; set; }
    
    public string? ImageUrl { get; set; }
}