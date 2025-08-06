using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LoreDrop.Tests.Services.User
{
    public class FavoriteServiceTests
    {
        private readonly LoreDropDbContext _context;
        private readonly IUserFavoritesRepository _favoritesRepository;
        private readonly ISeriesRepository _seriesRepository;
        private readonly FavoriteService _service;

        public FavoriteServiceTests()
        {
            var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new LoreDropDbContext(opts);
            _favoritesRepository = new UserFavoritesRepository(_context);
            _seriesRepository = new SeriesRepository(_context);
            _service = new FavoriteService(_favoritesRepository);
        }

        [Fact]
        public async Task AddAndRemoveFavorites_Works()
        {
            // Arrange
            var userId = "user1";
            var seriesId = Guid.NewGuid();
            _seriesRepository.Add(new Series 
            { 
                Id = seriesId, 
                Tittle = "Test Series", 
                Author = "Test Author", 
                Description = "Test Description" 
            });
            await _seriesRepository.SaveChangesAsync();
            await _seriesRepository.SaveChangesAsync();

            // Act - Add to favorites
            await _service.AddToFavoritesAsync(userId, seriesId.ToString());

            // Assert favorite added
            var favs = _favoritesRepository.GetAllAttached().ToList();
            Assert.Single(favs, f => f.UserId == userId && f.SeriesId == seriesId);

            // Act - Remove from favorites
            await _service.RemoveFromFavoritesAsync(userId, seriesId.ToString());

            // Assert favorite removed
            var favAfterRemove = _favoritesRepository.GetAllAttached().Where(f => f.UserId == userId && f.SeriesId == seriesId);
            Assert.Empty(favAfterRemove);
        }
    }
}
