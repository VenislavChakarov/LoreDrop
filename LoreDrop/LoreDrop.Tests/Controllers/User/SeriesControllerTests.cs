using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series;
using LoreDrop.Controllers;

namespace LoreDrop.Tests.Controllers
{
    public class SeriesControllerTests
    {
        private readonly Mock<ISeriesService> _seriesServiceMock;
        private readonly SeriesController _controller;

        public SeriesControllerTests()
        {
            _seriesServiceMock = new Mock<ISeriesService>();
            _controller = new SeriesController(_seriesServiceMock.Object);
        }

        [Fact]
        public async Task All_ReturnsView_WithSeries()
        {
           
            var fake = new List<AllSeriesIndexViewModel> { new AllSeriesIndexViewModel { Title = "S1" } };
            _seriesServiceMock.Setup(s => s.GetAllSeriesAsync()).ReturnsAsync((IEnumerable<AllSeriesIndexViewModel>)fake);
            
            var result = await _controller.Index();
            
            var vr = Assert.IsType<ViewResult>(result);
            Assert.NotNull(vr.Model);
            var enumModel = Assert.IsAssignableFrom<System.Collections.IEnumerable>(vr.Model);
            Assert.Single(enumModel.Cast<object>());
        }
    }
}