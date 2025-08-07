using Xunit;
using Moq;
using LoreDrop.Areas.Admin.Controllers;
using LoreDrop.Services.Core.Admin.Interface;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Admin.SeriesAdmin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using LoreDrop.Web.ViewModels.Series;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

public class DashboardControllerTests
{
    private readonly Mock<ISeriesAdminService> _seriesAdminServiceMock = new();
    private readonly Mock<IGenreService> _genreServiceMock = new();
    private readonly DashboardController _controller;

    public DashboardControllerTests()
    {
        _controller = new DashboardController(_seriesAdminServiceMock.Object, _genreServiceMock.Object);

        // Use the same claim type your controller actually reads (e.g. ClaimTypes.NameIdentifier)
        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        { 
            new Claim("sub", "test-user-id"), 
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
        
        var tempDataProvider = new Mock<ITempDataProvider>();
        _controller.TempData = new TempDataDictionary(
            _controller.ControllerContext.HttpContext,
            tempDataProvider.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithAllSeries()
    {
        var expectedId = Guid.NewGuid().ToString();
        var expected = new List<AllSeriesIndexViewModel>
        {
            new AllSeriesIndexViewModel
            {
                Id = expectedId,
                Title = "Test Series",
                Author = "Test Author",
                Genre = "Fantasy",
                Rating = 4.5,
                CreatedOn = "2023-01-01",
                ImageUrl = "/img.jpg",
                IsDeleted = false
            }
        };
        _seriesAdminServiceMock
            .Setup(s => s.GetAllSeriesAsync())
            .ReturnsAsync(expected);

        var result = await _controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<AllSeriesIndexViewModel>>(viewResult.Model);

        Assert.Single(model);
        Assert.Equal(expectedId, model.First().Id);
        Assert.Equal("Test Series", model.First().Title);
    }

    [Fact]
    public async Task Create_Get_ReturnsViewWithGenres()
    {
        var seededGenreId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var genres = new List<AddSeriesGenreDropDownMenu>
        {
            new AddSeriesGenreDropDownMenu { Id = seededGenreId, Name = "Action" }
        };
        _genreServiceMock
            .Setup(g => g.GetAllGenresAsync())
            .ReturnsAsync(genres);

        var result = await _controller.Create();

        var viewResult = Assert.IsType<ViewResult>(result);
        var vm = Assert.IsType<CreateSeriesFormViewModel>(viewResult.Model);

        Assert.NotNull(vm.Genres);
        var genre = Assert.Single(vm.Genres);
        Assert.Equal("Action", genre.Name);
        Assert.Equal(seededGenreId, genre.Id);
    }

    [Fact]
    public async Task Create_Post_InvalidModel_ReturnsViewWithGenres()
    {
        // force model‐state error
        _controller.ModelState.AddModelError("Title", "Required");

        var seededGenreId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var genres = new List<AddSeriesGenreDropDownMenu>
        {
            new AddSeriesGenreDropDownMenu { Id = seededGenreId, Name = "Fantasy" }
        };
        _genreServiceMock
            .Setup(g => g.GetAllGenresAsync())
            .ReturnsAsync(genres);

        var model = new CreateSeriesFormViewModel
        {
            Title = "",
            GenreId = seededGenreId
        };

        var result = await _controller.Create(model);

        var viewResult = Assert.IsType<ViewResult>(result);
        var returnedVm = Assert.IsType<CreateSeriesFormViewModel>(viewResult.Model);

        Assert.NotNull(returnedVm.Genres);
        var genre = Assert.Single(returnedVm.Genres);
        Assert.Equal("Fantasy", genre.Name);
        Assert.Equal(seededGenreId, genre.Id);
    }

    [Fact]
    public async Task Edit_Get_ReturnsViewResultWithModel()
    {
        var id = Guid.NewGuid();
        var editModel = new EditSerieViewModel
        {
            Id = id.ToString(),
            Title = "Test Title",
            Description = "Desc",
            Author = "Author",
            CreatedOn = "2023-01-01",
            GenreId = Guid.NewGuid(),
            Genres = new List<AddSeriesGenreDropDownMenu>()
        };
        _seriesAdminServiceMock
            .Setup(s => s.GetSeriesForEditAsync(id, It.IsAny<string>()))
            .ReturnsAsync(editModel);

        var result = await _controller.Edit(id);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(editModel, viewResult.Model);
    }

    [Fact]
    public async Task Edit_Post_InvalidModel_ReturnsView()
    {
        _controller.ModelState.AddModelError("error", "Invalid");
        var model = new EditSerieViewModel { /* leave invalid */ };

        var result = await _controller.Edit(model);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(model, viewResult.Model);
    }

    [Fact]
    public async Task Remove_ValidModel_SuccessfulDeletion()
    {
        var delVm = new DeleteSeriesViewModel { Id = Guid.NewGuid().ToString() };
        _seriesAdminServiceMock
            .Setup(s => s.SoftDeleteSeriesAsync(
                It.Is<DeleteSeriesViewModel>(m => m.Id == delVm.Id),
                It.IsAny<string>()))
            .ReturnsAsync(true);

        var result = await _controller.Remove(delVm);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }

    [Fact]
    public async Task Restore_ValidModel_SuccessfulRestore()
    {
        var restoreVm = new RestoreSeriesViewModel { Id = Guid.NewGuid().ToString() };
        _seriesAdminServiceMock
            .Setup(s => s.RestoreSeriesAsync(
                It.Is<RestoreSeriesViewModel>(m => m.Id == restoreVm.Id),
                It.IsAny<string>()))
            .ReturnsAsync(true);

        var result = await _controller.Restore(restoreVm);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }

    [Fact]
    public async Task HardDelete_Get_ValidId_ReturnsView()
    {
        var id = Guid.NewGuid();
        var vm = new HardDeleteSeriesViewModel
        {
            Id = id.ToString(),
            Tittle = "Test Title",
            Author = "Test Author"
        };
        _seriesAdminServiceMock
            .Setup(s => s.GetSeriesForHardDeleteAsync(id, It.IsAny<string>()))
            .ReturnsAsync(vm);

        var result = await _controller.HardDelete(id);

        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Same(vm, viewResult.Model);
    }

    [Fact]
    public async Task HardDelete_Post_ValidModel_DeletesSuccessfully()
    {
        var id = Guid.NewGuid();
        var vm = new HardDeleteSeriesViewModel { Id = id.ToString() };

        _seriesAdminServiceMock
            .Setup(s => s.HardDeleteSeriesAsync(
                It.Is<Guid>(g => g.ToString() == vm.Id),
                It.IsAny<string>()))
            .ReturnsAsync(true);

        var result = await _controller.HardDelete(vm);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
    }
}
