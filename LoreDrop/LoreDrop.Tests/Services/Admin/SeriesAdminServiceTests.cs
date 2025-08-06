using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core;
using LoreDrop.Services.Core.Admin;
using LoreDrop.Services.Core.Contracts;
using LoreDrop.Web.ViewModels.Admin.SeriesAdmin;

namespace LoreDrop.Tests.Services.Admin
{
    public class SeriesAdminServiceTests : IDisposable
    {
        private readonly LoreDropDbContext _context;
        private readonly ISeriesRepository seriesRepository;
        private readonly IGenreRepository genreRepository;
        private readonly SeriesAdminService _service;

        public SeriesAdminServiceTests()
        {
            var options = new DbContextOptionsBuilder<LoreDropDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LoreDropDbContext(options);
            seriesRepository = new SeriesRepository(_context);
            genreRepository = new GenreRepository(_context);
            IGenreService genreService = new GenreService(genreRepository);

            _service = new SeriesAdminService(seriesRepository, genreRepository, genreService);
        }

        [Fact]
        public async Task CreateSeriesAsync_ReturnsTrue_WhenDataIsValid()
        {
            // Arrange
            var genre = new Genre { Id = Guid.NewGuid(), Name = "Fantasy" };
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            var model = new CreateSeriesFormViewModel
            {
                Title = "Test",
                Description = "Desc",
                Author = "Auth",
                GenreId = genre.Id,
                CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                ImageUrl = "img"
            };

            // Act
            var result = await _service.CreateSeriesAsync(model, "user1");

            // Assert
            Assert.True(result);
            var added = _context.Series.FirstOrDefault(s => s.Tittle == "Test");
            Assert.NotNull(added);
            Assert.Equal(genre.Id, added.GenreId);
        }

        [Fact]
        public async Task CreateSeriesAsync_ReturnsFalse_WhenGenreMissing()
        {
            var model = new CreateSeriesFormViewModel
            {
                Title = "X",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-dd")
            };

            var result = await _service.CreateSeriesAsync(model, "user1");

            Assert.False(result);
            Assert.Empty(_context.Series);
        }

        [Fact]
        public async Task EditSeriesAsync_ReturnsTrue_WhenModelValid()
        {
            // Arrange
            var genre = new Genre { Id = Guid.NewGuid(), Name = "SciFi" };
            var series = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "Old",
                Description = "D",
                Author = "A",
                GenreId = genre.Id,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img"
            };
            await _context.Genres.AddAsync(genre);
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();

            var model = new EditSerieViewModel
            {
                Id = series.Id.ToString(),
                Title = "New",
                Description = "NewD",
                Author = "NewA",
                GenreId = genre.Id,
                CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                ImageUrl = "img2"
            };

            // Act
            var result = await _service.EditSeriesAsync(model, "user1");

            // Assert
            Assert.True(result);
            var updated = await _context.Series.FindAsync(series.Id);
            Assert.Equal("New", updated.Tittle);
        }

        [Fact]
        public async Task SoftDeleteSeriesAsync_MarksDeleted_WhenExists()
        {
            var series = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "T",
                Description = "D",
                Author = "A",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img",
                IsDeleted = false
            };
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();

            var model = new DeleteSeriesViewModel { Id = series.Id.ToString() };

            var result = await _service.SoftDeleteSeriesAsync(model, "user1");

            Assert.True(result);
            var s = await _context.Series.FindAsync(series.Id);
            Assert.True(s.IsDeleted);
        }

        [Fact]
        public async Task RestoreSeriesAsync_UnmarksDeleted_WhenExists()
        {
            var series = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "T",
                Description = "D",
                Author = "A",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img",
                IsDeleted = true
            };
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();

            var model = new RestoreSeriesViewModel { Id = series.Id.ToString() };

            var result = await _service.RestoreSeriesAsync(model, "user1");

            Assert.True(result);
            var s = await _context.Series.FindAsync(series.Id);
            Assert.False(s.IsDeleted);
        }

        [Fact]
        public async Task HardDeleteSeriesAsync_RemovesSeries_WhenExists()
        {
            var series = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "T",
                Description = "D",
                Author = "A",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img"
            };
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();

            var result = await _service.HardDeleteSeriesAsync(series.Id, "user1");

            Assert.True(result);
            var s = await _context.Series.FindAsync(series.Id);
            Assert.Null(s);
        }

        [Fact]
        public async Task GetSeriesForHardDeleteAsync_ReturnsViewModel()
        {
            var series = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "T",
                Description = "D",
                Author = "A",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img"
            };
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();

            var vm = await _service.GetSeriesForHardDeleteAsync(series.Id, "user1");

            Assert.NotNull(vm);
            Assert.Equal(series.Id.ToString(), vm.Id);
            Assert.Equal(series.Tittle, vm.Tittle);
        }

        [Fact]
        public async Task GetAllSeriesAsync_ReturnsAll()
        {
            // Arrange: seed genres first
            var genre1 = new Genre { Id = Guid.NewGuid(), Name = "Gen1" };
            var genre2 = new Genre { Id = Guid.NewGuid(), Name = "Gen2" };
            await _context.Genres.AddRangeAsync(genre1, genre2);

            var s1 = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "AAA",         // ≥3 chars
                Description = "Desc AAA", // ≥10 chars
                Author = "AuthA",       // ≥3 chars
                GenreId = genre1.Id,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img1"
            };
            var s2 = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "BBB",
                Description = "Desc BBB",
                Author = "AuthB",
                GenreId = genre2.Id,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img2"
            };
            await _context.Series.AddRangeAsync(s1, s2);
            await _context.SaveChangesAsync();

            // Act
            var list = await _service.GetAllSeriesAsync();

            // Assert
            Assert.Equal(2, list.Count());
        }


        [Fact]
        public async Task GetSeriesForEditAsync_ReturnsModel()
        {
            var genre = new Genre { Id = Guid.NewGuid(), Name = "G" };
            var series = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "T",
                Description = "D",
                Author = "A",
                GenreId = genre.Id,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img"
            };
            await _context.Genres.AddAsync(genre);
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();

            var edit = await _service.GetSeriesForEditAsync(series.Id, "user1");

            Assert.NotNull(edit);
            Assert.Equal(series.Tittle, edit.Title);
            Assert.Contains(genre.Id, edit.Genres.Select(g => g.Id));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
