using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Web.ViewModels.Series;

public class CommentInputViewModel
{
    public CommentInputViewModel()
    {
        this.CreatedOn = DateTime.UtcNow.ToString(DateFormat);
    }
    
    public int SeriesId { get; set; }
    
    public string Text { get; set; } = null!;
    
    public string Publisher { get; set; } = null!;
    
    public string CreatedOn { get; set; }
    
}