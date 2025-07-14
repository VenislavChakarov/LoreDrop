using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Mvc;

namespace LoreDrop.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DetailsController : BaseController
{
    private readonly IDetailsService detailsService;
    private readonly ICommentService commentService;
    

    public DetailsController(
        IDetailsService detailsService,
        ICommentService commentService)
    {
        this.detailsService  = detailsService;
        this.commentService = commentService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || id <= 0) return RedirectToAction(nameof(Index));

        var userId = this.GetUserId();
        var seriesDetails = await detailsService.GetSeriesDetailsAsync(id.Value, userId);
        if (seriesDetails == null) return RedirectToAction(nameof(Index));

        seriesDetails.Comments = await commentService.GetCommentsBySeriesIdAsync(id.Value);
        return View("../Series/Details", seriesDetails);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(int seriesId, CommentInputViewModel commentInput)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Details), new { id = seriesId });

        var added = await commentService.AddCommentAsync(commentInput, GetUserId(), seriesId);
        if (!added)
        {
            ModelState.AddModelError("", "Failed to add comment. Please try again.");
        }
        return RedirectToAction(nameof(Details), new { id = seriesId });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCommentAjax([FromBody] CommentAjaxReqestViewModel req)
    {
        if (string.IsNullOrWhiteSpace(req.Text))
            return BadRequest("Comment text is required.");

        var comment = await commentService
            .AddCommentAndReturnAsync(req.SeriesId, GetUserId(), req.Text);

        return Json(new {
            authorName = comment.AuthorName,
            text       = comment.Text,
            createdOn  = comment.CreatedOn.ToString("yyyy-MM-dd")
        });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Rate([FromBody] RateRequestViewModel req)
    {
        if (req.Rating < 0.5 || req.Rating > 5 || (req.Rating * 2) % 1 != 0)
            return BadRequest("Rating must be in half-star increments between 0.5 and 5.");

        await detailsService.SetRatingAsync(req.SeriesId, req.Rating, GetUserId());
        return Ok();
    }
    
}
