using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Tests.Services.User
{
    public class CommentServiceRepoTests : IDisposable
    {
        private readonly LoreDropDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CommentService _service;
        private readonly List<IdentityUser> _users = new();

        public CommentServiceRepoTests()
        {
            var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new LoreDropDbContext(opts);

            // Create mocked UserManager that returns users from _users list
            var userManagerMock = MockUserManager(_users);
            _userManager = userManagerMock.Object;

            ICommentsRepository repo = new CommentsRepository(_context);
            _service = new CommentService(repo, _userManager);
        }

        [Fact]
        public async Task GetCommentsBySeriesIdAsync_MapsCorrectly()
        {
            // Arrange
            var seriesId = Guid.NewGuid();
            var user = new IdentityUser { Id = Guid.NewGuid().ToString(), UserName = "u1@gmail.com" };
            _users.Add(user);  // Add to the mocked UserManager users list
            _context.Users.Add(user);

            _context.Series.Add(new Series
            {
                Id = seriesId,
                Tittle = "Title",
                Author = "Author Name",
                Description = "Some description",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "image.jpg"
            });

            _context.Comments.Add(new Comments
            {
                SeriesId = seriesId,
                Text = "test",
                UserId = user.Id,
                User = user
            });

            await _context.SaveChangesAsync();

            // Act
            var comments = await _service.GetCommentsBySeriesIdAsync(seriesId);

            // Assert
            Assert.Single(comments);
            var comment = comments.First();
            Assert.Equal("test", comment.Text);
            Assert.Equal("u1", comment.AuthorName); // Username before '@'
        }

        public void Dispose() => _context.Dispose();

        // Helper method to mock UserManager
        private static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> users) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            mgr.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) =>
                    users.FirstOrDefault(u => (u as IdentityUser)?.Id == id));

            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}
