namespace LoreDrop.Web.ViewModels.WatchList;

public class WatchListViewModel
{
    public string SeriesId { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public int StateId { get; set; }
    
    public IEnumerable<AddSeriesStateDropDownMenu> StateDropDownMenu { get; set; } = null!;
    
    public string? ImageUrl { get; set; }
}