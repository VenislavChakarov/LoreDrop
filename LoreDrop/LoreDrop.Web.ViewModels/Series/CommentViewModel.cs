namespace LoreDrop.Web.ViewModels.Series;

public class CommentViewModel
{
    public string AuthorName { get; set; } = null!;
    public string Text { get; set; } = null!;
    
    public DateTime CreatedOn { get; set; }
    
}
