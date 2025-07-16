using LoreDrop.Web.ViewModels.Series;

namespace LoreDrop.Services.Core.Contracts;

public interface ICommentService
{
    Task<bool> AddCommentAsync(CommentInputViewModel commentInput, string? userId, Guid seriesId);
    Task<List<CommentViewModel>> GetCommentsBySeriesIdAsync(Guid seriesId);
    Task<CommentViewModel> AddCommentAndReturnAsync(Guid seriesId, string userId, string text);
}