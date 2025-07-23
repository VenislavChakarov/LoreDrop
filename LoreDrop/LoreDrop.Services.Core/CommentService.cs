using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Services.Core;

public class CommentService : ICommentService
{
    private readonly ICommentsRepository commentsRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public CommentService(ICommentsRepository context, UserManager<IdentityUser> userManager)
    {
        commentsRepository = context;
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
                UserId = userId,  // Set the foreign key explicitly
                User = user,      // Also set the navigation property
                Text = commentInput.Text,
                SeriesId = seriesId,
                CreatedOn = DateTime.UtcNow
            };

            await commentsRepository.AddAsync(comment);
            optResult = true;
        }

        return optResult;
    }

    public async Task<List<CommentViewModel>> GetCommentsBySeriesIdAsync(Guid seriesId)
    {
        var raw = await commentsRepository.GetAllAttached()
            .Where(c => c.SeriesId == seriesId)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new
            {
                c.Text,
                c.CreatedOn,
                UserName = c.User.UserName
            })
            .ToListAsync();

       
        return raw.Select(x => new CommentViewModel
            {
                AuthorName = x.UserName.Split('@')[0],
                Text       = x.Text,
                CreatedOn  = x.CreatedOn
            })
            .ToList();
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

        await commentsRepository.AddAsync(comment);

        return new CommentViewModel
        {
            AuthorName = user.UserName,
            Text = comment.Text,
            CreatedOn = comment.CreatedOn
        };
    }
}