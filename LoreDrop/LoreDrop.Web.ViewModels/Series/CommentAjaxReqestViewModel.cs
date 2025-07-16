namespace LoreDrop.Web.ViewModels.Series;

public class CommentAjaxReqestViewModel
{
    public Guid SeriesId { get; set; }
    
    public string Text { get; set; } = String.Empty;
}