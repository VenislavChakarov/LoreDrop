using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class CommentService : ICommentService
{
    private readonly LoreDropDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CommentService(LoreDropDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<bool> AddCommentAsync(CommentInputViewModel? commentInput, string? userId, Guid seriesId)
    {
        bool optResult = false;

        IdentityUser? user = await _userManager.FindByIdAsync(userId);
        if (user != null && commentInput != null)
        {
            var comment = new Comments
            {
                User = user,
                Text = commentInput.Text,
                SeriesId = seriesId,
                CreatedOn = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            optResult = true;
        }

        return optResult;
    }

    public async Task<List<CommentViewModel>> GetCommentsBySeriesIdAsync(Guid seriesId)
    {
        return await _context.Comments
            .Where(c => c.SeriesId == seriesId)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new CommentViewModel
            {
                AuthorName = c.User.UserName,
                Text = c.Text,
                CreatedOn = c.CreatedOn
            })
            .ToListAsync();
    }

    public async Task<CommentViewModel> AddCommentAndReturnAsync(Guid seriesId, string userId, string text)
    {
        IdentityUser? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        var comment = new Comments
        {
            User = user,
            Text = text,
            SeriesId = seriesId,
            CreatedOn = DateTime.UtcNow
        };

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return new CommentViewModel
        {
            AuthorName = user.UserName,
            Text = comment.Text,
            CreatedOn = comment.CreatedOn
        };
    }
}
