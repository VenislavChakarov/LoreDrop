using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LoreDrop.Controllers;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web;
using LoreDrop.Web.ViewModels.WatchList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LoreDrop.Tests.Controllers.User
{
    public class WatchListControllerTests
    {
        private readonly Mock<IWatchListService> _watchListServiceMock;
        private readonly WatchListController _controller;

        public WatchListControllerTests()
        {
            _watchListServiceMock = new Mock<IWatchListService>();
            _controller = new WatchListController(_watchListServiceMock.Object);
        }

        private void SetUser(string userId)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "TestAuth"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task Index_ReturnsViewWithWatchlist()
        {
            
            var userId = "user1";
            SetUser(userId);

            var list = new List<WatchListViewModel>
            {
                new WatchListViewModel { SeriesId = Guid.NewGuid().ToString(), Title = "X" }
            };

            _watchListServiceMock.Setup(s => s.GetAllAsync(userId))
                                 .ReturnsAsync(list);
            
            var result = await _controller.Index();
            
            var vr = Assert.IsType<ViewResult>(result);
            Assert.Same(list, vr.Model);
        }

        [Fact]
        public async Task AddToWatchList_AddsWhenNotPresent_AndRedirectsToIndex()
        {
            var userId = "user1";
            SetUser(userId);

            var seriesId = Guid.NewGuid().ToString();

            _watchListServiceMock.Setup(s => s.IsSeriesInWatchListAsync(userId, seriesId))
                                 .ReturnsAsync(false);

            _watchListServiceMock.Setup(s => s.AddToWatchListAsync(userId, seriesId))
                                 .Returns(Task.CompletedTask)
                                 .Verifiable();

            
            var result = await _controller.AddToWatchList(seriesId);

            
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _watchListServiceMock.Verify(s => s.AddToWatchListAsync(userId, seriesId), Times.Once);
        }

        [Fact]
        public async Task ChangeState_UpdatesAndRedirects()
        {
            
            var userId = "user1";
            SetUser(userId);

            var seriesId = Guid.NewGuid().ToString();
            var stateId = Guid.NewGuid();

            _watchListServiceMock.Setup(s => s.ChageSateAsync(seriesId, userId, stateId))
                                 .Returns(Task.CompletedTask)
                                 .Verifiable();

            
            var result = await _controller.ChangeState(seriesId, stateId);

            
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            _watchListServiceMock.Verify(s => s.ChageSateAsync(seriesId, userId, stateId), Times.Once);
        }

        [Fact]
        public async Task RemoveFromWatchList_RemovesAndRedirects()
        {
            
            var userId = "user1";
            SetUser(userId);

            var seriesId = Guid.NewGuid();
            var seriesIdStr = seriesId.ToString();

            
            _watchListServiceMock
                .Setup(s => s.IsSeriesInWatchListAsync(userId, seriesIdStr))
                .ReturnsAsync(true);

            _watchListServiceMock
                .Setup(s => s.RemoveFromWatchListAsync(userId, seriesIdStr))
                .Returns(Task.CompletedTask);

            
            var result = await _controller.RemoveFromWatchList(seriesIdStr);

            
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            
            _watchListServiceMock.Verify(s => s.RemoveFromWatchListAsync(userId, seriesIdStr), Times.Once);
        }
    }
}
