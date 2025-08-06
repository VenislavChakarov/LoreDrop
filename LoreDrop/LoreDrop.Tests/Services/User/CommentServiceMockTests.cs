using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core;
using LoreDrop.Web.ViewModels.Series;

using Microsoft.AspNetCore.Identity;
using Moq;

namespace LoreDrop.Tests.Services.User
{
    // Helper to mock UserManager
public static class MockUserManager
{
    public static Mock<UserManager<TUser>> Create<TUser>() where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
    }
}

public class CommentServiceMockTests
    {
        private readonly CommentService _service;
        private readonly Mock<ICommentsRepository> _repoMock;
        private readonly Mock<UserManager<IdentityUser>> _userMgrMock;

        public CommentServiceMockTests()
        {
            _repoMock = new Mock<ICommentsRepository>();
            _userMgrMock = MockUserManager.Create<IdentityUser>();
            _service = new CommentService(_repoMock.Object, _userMgrMock.Object);
        }

        [Fact]
        public async Task AddCommentAsync_ReturnsTrue_WhenValid()
        {
            var user = new IdentityUser { Id = "u1", UserName = "u1@test" };
            _userMgrMock.Setup(u => u.FindByIdAsync("u1")).ReturnsAsync(user);
            var vm = new CommentInputViewModel { Text = "Hello" };
            var sid = Guid.NewGuid();

            var result = await _service.AddCommentAsync(vm, "u1", sid);

            Assert.True(result);
            _repoMock.Verify(r => r.AddAsync(It.Is<Comments>(c => c.Text == "Hello" && c.UserId == "u1" && c.SeriesId == sid)), Times.Once);
        }

        [Fact]
        public async Task AddCommentAndReturnAsync_Throws_WhenUserNotFound()
        {
            _userMgrMock.Setup(u => u.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);
            await Assert.ThrowsAsync<Exception>(() => _service.AddCommentAndReturnAsync(Guid.Empty, "u1", "text"));
        }
    }
}
