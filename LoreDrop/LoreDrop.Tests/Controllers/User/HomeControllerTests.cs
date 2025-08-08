using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Home; 
using LoreDrop.Controllers; 

namespace LoreDrop.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IHomeService> _homeServiceMock;
        private readonly Mock<ILogger<HomeController>> _loggerMock;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _homeServiceMock = new Mock<IHomeService>();
            _loggerMock = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_loggerMock.Object, _homeServiceMock.Object);
        }

        [Fact]
        public async Task Index_ReturnsView_WithTopRatedModel()
        {
            var fakeList = new List<TopRatedSeries> { new TopRatedSeries { Tittle = "AAA" } };
            _homeServiceMock.Setup(h => h.GetTopRatedSeriesAsync())
                .ReturnsAsync((IEnumerable<TopRatedSeries>)fakeList);

           
            var result = await _controller.Index();
            
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
            var enumerable = Assert.IsAssignableFrom<System.Collections.IEnumerable>(viewResult.Model);
            Assert.Single(enumerable.Cast<object>());
        }
    }
}