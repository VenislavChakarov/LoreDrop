using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    public async Task<IActionResult> Details(Guid? id)
    {
        try
        {
            if (id == null || id == Guid.Empty) return RedirectToAction(nameof(Index));
            
            var seriesDetails = await detailsService.GetSeriesDetailsAsync(id);
            if (seriesDetails == null) return RedirectToAction(nameof(Index));

            seriesDetails.Comments = await commentService.GetCommentsBySeriesIdAsync(id.Value);
            return View("../Series/Details", seriesDetails);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index));
        }
        
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(Guid seriesId, CommentInputViewModel commentInput)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            ModelState.AddModelError("", "An error occurred while adding the comment. Please try again.");
            return RedirectToAction(nameof(Details), new { id = seriesId });
        }
        
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCommentAjax([FromBody] CommentAjaxReqestViewModel req)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Details), new { id = req.SeriesId });
        }
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
