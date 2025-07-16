using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoreDrop.Data.Configuration;

public class SeriesStateConfiguration : IEntityTypeConfiguration<SeriesState>
{
    public void Configure(EntityTypeBuilder<SeriesState> entity)
    {
        entity
            .HasKey(ss => ss.Id);

        entity
            .Property(ss => ss.Name)
            .IsRequired()
            .HasMaxLength(50);

        entity
            .HasData(this.GetSeriesStateData());
    }
    
    private List<SeriesState> GetSeriesStateData()
    {
        return new List<SeriesState>
        {
            new SeriesState { Id = 1, Name = "Ongoing" },
            new SeriesState { Id = 2, Name = "Completed" },
            new SeriesState { Id = 3, Name = "Cancelled" }
        };
    }
}