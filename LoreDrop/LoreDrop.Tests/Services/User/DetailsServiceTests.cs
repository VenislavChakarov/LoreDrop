using System;
using System.Threading.Tasks;
using LoreDrop.Data;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Data.Repository;
using LoreDrop.Services.Core;
using LoreDrop.Services.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Tests.Services.User
{
    public class DetailsServiceTests : IDisposable
    {
        private readonly LoreDropDbContext _context;
        private readonly DetailsService _service;

        public DetailsServiceTests()
        {
            var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new LoreDropDbContext(opts);

            ISeriesRepository seriesRepo = new SeriesRepository(_context);
            ISeriesRatingRepository ratingRepo = new SeriesRatingRepository(_context);

            var favMock = new Mock<IFavoriteService>();
            var watchMock = new Mock<IWatchListService>();

            _service = new DetailsService(seriesRepo, favMock.Object, watchMock.Object, ratingRepo);
        }

        [Fact]
        public async Task GetSeriesDetailsAsync_ReturnsNull_WhenIdNull()
        {
            var vm = await _service.GetSeriesDetailsAsync(null, null);
            Assert.Null(vm);
        }

        [Fact]
        public async Task SetRatingAsync_AddsThenUpdates()
        {
            var sid = Guid.NewGuid();

            // Add Series with all required fields
            await _context.Series.AddAsync(new Series
            {
                Id = sid,
                Tittle = "AAA",
                Description = "Description AAA",
                Author = "Auth",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "img"
            });
            await _context.SaveChangesAsync();

            // Add rating 4
            await _service.SetRatingAsync(sid, 4, "u1");
            var rating = await _context.SeriesRatings.SingleAsync();
            Assert.Equal(4, rating.Rating);

            // Update rating to 5
            await _service.SetRatingAsync(sid, 5, "u1");
            rating = await _context.SeriesRatings.SingleAsync();
            Assert.Equal(5, rating.Rating);
        }

        public void Dispose() => _context.Dispose();
    }
}
