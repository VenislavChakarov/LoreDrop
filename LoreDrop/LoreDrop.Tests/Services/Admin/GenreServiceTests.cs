
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core;
using LoreDrop.Services.Core.Contracts;

namespace LoreDrop.Tests.Services.Admin
{
    public class GenreServiceTests : IDisposable
    {
        private readonly LoreDropDbContext _context;
        private readonly IGenreService _genreService;

        public GenreServiceTests()
        {
            var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new LoreDropDbContext(opts);
            _genreService = new GenreService(new GenreRepository(_context));
        }

        [Fact]
        public async Task GetAllGenresAsync_ReturnsAllSeeded()
        {
            // Arrange
            var g1 = new Genre { Id = Guid.NewGuid(), Name = "Fantasy" };
            var g2 = new Genre { Id = Guid.NewGuid(), Name = "Mystery" };
            await _context.Genres.AddRangeAsync(g1, g2);
            await _context.SaveChangesAsync();

            // Act
            var list = await _genreService.GetAllGenresAsync();

            // Assert
            Assert.Equal(2, list.Count());
            Assert.Contains(list, x => x.Id == g1.Id && x.Name == g1.Name);
            Assert.Contains(list, x => x.Id == g2.Id && x.Name == g2.Name);
        }

        public void Dispose() => _context.Dispose();
    }
}