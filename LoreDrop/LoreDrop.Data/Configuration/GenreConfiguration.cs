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
            new Genre { Id = 1, Name = "Fantasy" },
            new Genre { Id = 2, Name = "Science Fiction" },
            new Genre { Id = 3, Name = "Mystery" },
            new Genre { Id = 4, Name = "Romance" },
            new Genre { Id = 5, Name = "Horror" }
        };
    }
}