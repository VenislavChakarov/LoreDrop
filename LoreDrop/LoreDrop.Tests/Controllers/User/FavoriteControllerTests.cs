using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Controllers;
using System.Security.Claims;
using LoreDrop.Web.ViewModels.Favorites;
using Microsoft.AspNetCore.Http;

namespace LoreDrop.Tests.Controllers.User
{
    public class FavoriteControllerTests
    {
        private readonly Mock<IFavoriteService> _favoriteServiceMock;
        private readonly FavoriteController _controller;

        public FavoriteControllerTests()
        {
            _favoriteServiceMock = new Mock<IFavoriteService>();
            _controller = new FavoriteController(_favoriteServiceMock.Object);
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
        public async Task Index_ReturnsViewWithFavorites()
        {
            
            var userId = "user1";
            SetUser(userId);

            var favorites = new List<FavoriteSereisViewModel>
            {
                new FavoriteSereisViewModel { SeriesId = Guid.NewGuid().ToString(), Title = "S1" }
            };

            _favoriteServiceMock
                .Setup(s => s.GetUserFavoritesAsync(userId))
                .ReturnsAsync(favorites);

            
            var result = await _controller.Index();

            
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelEnumerable = Assert.IsAssignableFrom<IEnumerable<FavoriteSereisViewModel>>(viewResult.Model);
            var modelList = modelEnumerable.ToList();

            Assert.Single(modelList);
            Assert.Equal(favorites[0].SeriesId, modelList[0].SeriesId);
            Assert.Equal(favorites[0].Title, modelList[0].Title);
        }
    }
}
