using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Series; 
using LoreDrop.Controllers; 

namespace LoreDrop.Tests.Controllers.User
{
    public class DetailsControllerTests
    {
        private readonly Mock<IDetailsService> _detailsMock;
        private readonly Mock<ICommentService> _commentMock;
        private readonly DetailsController _controller;

        public DetailsControllerTests()
        {
            _detailsMock = new Mock<IDetailsService>();
            _commentMock = new Mock<ICommentService>();
            _controller = new DetailsController(_detailsMock.Object, _commentMock.Object);
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "u1")
            }, "TestAuth"));
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task Index_ReturnsNotFound_WhenServiceReturnsNull()
        {
           
            var id = Guid.NewGuid();
            _detailsMock.Setup(d => d.GetSeriesDetailsAsync(id, It.IsAny<string>()))
                        .ReturnsAsync((SeriesDetailesViewModel?)null); // TODO: adjust type name if different
            
            var result = await _controller.Details(id);
            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Index_ReturnsView_WithModel_WhenFound()
        {
            
            var id = Guid.NewGuid();
            var vm = new SeriesDetailesViewModel { Id = id.ToString() }; // TODO: adjust property names if different
            _detailsMock.Setup(d => d.GetSeriesDetailsAsync(id, It.IsAny<string>()))
                        .ReturnsAsync(vm);

            
            var result = await _controller.Details(id);

            
            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal(vm, view.Model);
        }
    }
}
