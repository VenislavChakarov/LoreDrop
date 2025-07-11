using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LoreDrop.GCommon.ValidationConstants.Series;

namespace LoreDrop.Data.Configuration;

public class SeriesConfiguration : IEntityTypeConfiguration<Series>
{
    public void Configure(EntityTypeBuilder<Series> entity)
    {
        entity
            .HasKey(s => s.Id);
        
        entity
            .Property(s => s.Tittle)
            .IsRequired()
            .HasMaxLength(TitleMaxLength);
        
        entity
            .Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        entity
            .Property(s => s.Author)
            .IsRequired()
            .HasMaxLength(AuthorNameMaxLength);
        
        entity
            .Property(s => s.Rating)
            .IsRequired(false);
        
        entity
            .Property(s => s.ImageUrl)
            .IsRequired(false);
        
        entity
            .Property(s => s.CreatedOn)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);
        
        entity
            .Property(s => s.IsDeleted)
            .HasDefaultValue(false);
        
        entity
            .HasQueryFilter(s => s.IsDeleted == false);


        entity
            .HasOne(s => s.Genre)
            .WithMany(g => g.Series)
            .HasForeignKey(s => s.GenreId);

        entity
            .HasData(this.GetPredefinedSeries());
    }

    private List<Series> GetPredefinedSeries()
    {
        return new List<Series>
        {
            new Series
            {
                Id = 1,
                Tittle = "The Hobbit",
                Description = "A fantasy novel by J.R.R. Tolkien.",
                Author = "J.R.R. Tolkien",
                Rating = 4.8,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/4/4a/TheHobbit_FirstEdition.jpg",
                CreatedOn = DateTime.UtcNow,
                GenreId = 1,
                IsDeleted = false
            },
            new Series
            {
                Id = 2,
                Tittle = "Dune",
                Description = "A science fiction novel by Frank Herbert.",
                Author = "Frank Herbert",
                Rating = 4.7,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/a/a8/Dune_First_Edition.jpg",
                CreatedOn = DateTime.UtcNow,
                GenreId = 2,
                IsDeleted = false
            },
            new Series
            {
                Id = 3,
                Tittle = "Dracula",
                Description = "A gothic horror novel by Bram Stoker.",
                Author = "Bram Stoker",
                Rating = 4.5,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/3/35/Dracula1st.jpeg",
                CreatedOn = DateTime.UtcNow,
                GenreId = 5,
                IsDeleted = false
            },
            new Series
            {
                Id = 4,
                Tittle = "The Eye of Argon",
                Description = "A science fiction novella often cited as one of the worst works of literature ever published.",
                Author = "Jim Theis",
                Rating = 1.5,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/en/3/3f/Eye_of_Argon.jpg",
                CreatedOn = DateTime.UtcNow,
                GenreId = 3,
                IsDeleted = false
            }
        };
    }
}