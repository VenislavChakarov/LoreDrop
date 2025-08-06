using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Tests.Services.User;

public class SeriesStateServiceTests
{
    private readonly ISeriesStateRepository seriesStateRepository;
    private readonly SeriesStateService _service;

    public SeriesStateServiceTests()
    {
        var opts = new DbContextOptionsBuilder<LoreDropDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        seriesStateRepository = new SeriesStateRepository(new LoreDropDbContext(opts));
        _service = new SeriesStateService(seriesStateRepository);
    }

    [Fact]
    public async Task GetAllStatesAsync_ReturnsStates()
    {
        var state = new SeriesState { Id = Guid.NewGuid(), Name = "Ongoing" };
        await seriesStateRepository.AddAsync(state);
        await seriesStateRepository.SaveChangesAsync();

        var list = await _service.GetAllStatesAsync();
        Assert.Single(list);
        Assert.Equal(state.Id, list.First().Id);
    }
}