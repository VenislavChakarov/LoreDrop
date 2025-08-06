using System;
using System.Linq;
using System.Threading.Tasks;
using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LoreDrop.Tests.Services.User
{
    public class HomeServiceTests : IDisposable
    {
        private readonly LoreDropDbContext _context;
        private readonly HomeService _service;

        public HomeServiceTests()
        {
            var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LoreDropDbContext(opts);

            ISeriesRepository repo = new SeriesRepository(_context);
            _service = new HomeService(repo);
        }

        [Fact]
        public async Task GetTopRatedSeriesAsync_ReturnsSorted()
        {
            var genre = new Genre { Id = Guid.NewGuid(), Name = "G1" };
            _context.Genres.Add(genre);

            var s1 = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "AAA",
                Description = "DescAAA",
                Author = "A",
                GenreId = genre.Id,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img"
            };
            var s2 = new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "BBB",
                Description = "DescBBB",
                Author = "B",
                GenreId = genre.Id,
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img"
            };

            _context.Series.AddRange(s1, s2);

            _context.SeriesRatings.AddRange(
                new SeriesRating { SeriesId = s1.Id, UserId = "u1", Rating = 5 },
                new SeriesRating { SeriesId = s2.Id, UserId = "u1", Rating = 3 });

            await _context.SaveChangesAsync();

            var list = (await _service.GetTopRatedSeriesAsync()).ToList();

            Assert.Equal(2, list.Count);
            Assert.Equal("AAA", list[0].Tittle);
        }

        public void Dispose() => _context.Dispose();
    }
}
