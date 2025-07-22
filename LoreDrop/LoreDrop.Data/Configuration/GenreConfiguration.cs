using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LoreDrop.GCommon.ValidationConstants.Genre;

namespace LoreDrop.Data.Configuration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> entity)
    {
        entity
            .HasKey(g => g.Id);
        
        entity
            .Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);
        
        entity
          .HasData(this.GetPredefinedGenres());
    }

    private List<Genre> GetPredefinedGenres()
    {
        return new List<Genre>
        {
            new Genre { Id = Guid.NewGuid(), Name = "Fantasy" },
            new Genre { Id = Guid.NewGuid(), Name = "Science Fiction" },
            new Genre { Id = Guid.NewGuid(), Name = "Mystery" },
            new Genre { Id = Guid.NewGuid(), Name = "Romance" },
            new Genre { Id = Guid.NewGuid(), Name = "Horror" }
        };
    }
}