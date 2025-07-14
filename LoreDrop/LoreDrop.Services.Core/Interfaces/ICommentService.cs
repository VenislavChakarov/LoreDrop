using LoreDrop.Web.ViewModels.Series;

namespace LoreDrop.Services.Core.Contracts;

public interface ICommentService
{
    Task<bool> AddCommentAsync(CommentInputViewModel commentInput, string? userId, int seriesId);
    Task<List<CommentViewModel>> GetCommentsBySeriesIdAsync(int seriesId);
    Task<CommentViewModel> AddCommentAndReturnAsync(int seriesId, string userId, string text);
}