namespace LoreDrop.Web.ViewModels.Series;

public class CommentAjaxReqestViewModel
{
    public int SeriesId { get; set; }
    
    public string Text { get; set; } = String.Empty;
}