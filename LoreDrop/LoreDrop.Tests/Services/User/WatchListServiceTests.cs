using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Tests.Services.User
{
    public class WatchListServiceTests : IDisposable
    {
        private readonly LoreDropDbContext _context;
        private readonly IUserWatchListRepository _watchListRepository;
        private readonly ISeriesStateRepository _stateRepository;
        private readonly WatchListService _service;

        public WatchListServiceTests()
        {
            var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LoreDropDbContext(opts);
            _watchListRepository = new UserWatchListRepository(_context);
            _stateRepository = new SeriesStateRepository(_context);
            _service = new WatchListService(_watchListRepository, _stateRepository);
        }

        [Fact]
        public async Task AddRemoveAndChangeState_Works()
        {
            // Arrange
            var userId = "test-user";
            var seriesId = Guid.NewGuid();

            // Seed SeriesState "Ongoing"
            var ongoingState = new SeriesState { Id = Guid.NewGuid(), Name = "Ongoing" };
            _context.SeriesStates.Add(ongoingState);

            // Seed a Series (required for navigation property)
            _context.Series.Add(new Series
            {
                Id = seriesId,
                Tittle = "Some Title",
                Author = "Author",
                Description = "Description",
                GenreId = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                ImageUrl = "image.jpg"
            });

            await _context.SaveChangesAsync();

            // Act: Add to watchlist
            await _service.AddToWatchListAsync(userId, seriesId.ToString());

            // Assert: it is in watchlist now
            Assert.True(await _service.IsSeriesInWatchListAsync(userId, seriesId.ToString()));

            // Act: Change state
            await _service.ChageSateAsync(seriesId.ToString(), userId, ongoingState.Id);

            // Act: Remove from watchlist
            await _service.RemoveFromWatchListAsync(userId, seriesId.ToString());

            // Assert: watchlist is empty for user
            var allEntries = _watchListRepository.GetAllAttached().Where(w => w.UserId == userId);
            Assert.Empty(allEntries);

            Assert.False(await _service.IsSeriesInWatchListAsync(userId, seriesId.ToString()));
        }

        public void Dispose() => _context.Dispose();
    }
}
