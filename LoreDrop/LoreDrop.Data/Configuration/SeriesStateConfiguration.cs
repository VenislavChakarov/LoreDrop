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
            new SeriesState { Id = new Guid(), Name = "Ongoing" },
            new SeriesState { Id = new Guid(), Name = "Completed" },
            new SeriesState { Id = new Guid(), Name = "Cancelled" }
        };
    }
}