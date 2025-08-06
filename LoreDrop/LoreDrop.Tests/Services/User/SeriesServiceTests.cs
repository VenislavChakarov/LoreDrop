using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Tests.Services.User;

public class SeriesServiceTests
{
    private readonly LoreDropDbContext _context;
    private readonly ISeriesRepository _seriesRepository;
    private readonly SeriesService _service;

    public SeriesServiceTests()
    {
        var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new LoreDropDbContext(opts);

        _seriesRepository = new SeriesRepository(_context);
        _service = new SeriesService(_seriesRepository);
    }

    [Fact]
    public async Task GetAllSeriesAsync_ReturnsSeries()
    {
        var genre = new Genre { Id = Guid.NewGuid(), Name = "GenX" };
        _context.Genres.Add(genre);
        _context.Series.Add(new Series {
            Id = Guid.NewGuid(),
            Tittle = "AAA",
            Description = "Description AAA",
            Author = "Auth",
            GenreId = genre.Id,
            CreatedOn = DateTime.UtcNow,
            ImageUrl = "img"
        });
        await _context.SaveChangesAsync();

        var list = await _service.GetAllSeriesAsync();

        Assert.Single(list);
    }

}