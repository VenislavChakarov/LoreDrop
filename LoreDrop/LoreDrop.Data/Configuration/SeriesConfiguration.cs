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
        
       // entity
         // .HasData(this.GetPredefinedSeries());
    }

    private List<Series> GetPredefinedSeries()
    {
        return new List<Series>
        {
            new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "The Chronicles of LoreDrop",
                Description = "An epic fantasy series exploring the mysteries of the LoreDrop universe.",
                Author = "Jane Doe",
                Rating = 4.8,
                ImageUrl = "https://images.unsplash.com/photo-1506744038136-46273834b3fb?auto=format&fit=crop&w=800&q=80",
                CreatedOn = new DateTime(2024, 7, 16),
                GenreId = Guid.NewGuid(),
                IsDeleted = false
            },
            new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "Spacebound: The Last Frontier",
                Description = "Follow the crew of the starship Horizon as they journey through uncharted galaxies, facing cosmic threats and unraveling the secrets of ancient civilizations. This sci-fi saga blends hard science with thrilling adventure and deep philosophical questions about humanity's place in the universe.",
                Author = "John Smith",
                Rating = 4.6,
                ImageUrl = "https://images.unsplash.com/photo-1465101046530-73398c7f28ca?auto=format&fit=crop&w=800&q=80",
                CreatedOn = new DateTime(2023, 11, 2),
                GenreId = Guid.NewGuid(),
                IsDeleted = false
            },
            new Series
            {
                Id =Guid.NewGuid(),
                Tittle = "Mysteries of the Forgotten Realms",
                Description = "Dive into a world where magic is real, kingdoms rise and fall, and ancient secrets wait to be discovered. Each season uncovers new lands, legendary heroes, and dark forces threatening the balance of the realms. Richly detailed lore and character-driven storytelling make this fantasy series a must-watch for genre fans.",
                Author = "Emily Carter",
                Rating = 4.9,
                ImageUrl = "https://images.unsplash.com/photo-1500534314209-a25ddb2bd429?auto=format&fit=crop&w=800&q=80",
                CreatedOn = new DateTime(2022, 5, 20),
                GenreId = Guid.NewGuid(),
                IsDeleted = false
            },
            new Series
            {
                Id = Guid.NewGuid(),
                Tittle = "Echoes of Tomorrow",
                Description = "A gripping dystopian drama set in a future where memories can be traded, stolen, and rewritten. The story follows rebels fighting against a totalitarian regime that controls the past and the future. Complex characters, moral dilemmas, and a haunting vision of technology gone awry define this series.",
                Author = "Michael Lee",
                Rating = 4.7,
                ImageUrl = "https://images.unsplash.com/photo-1519125323398-675f0ddb6308?auto=format&fit=crop&w=800&q=80",
                CreatedOn = new DateTime(2025, 1, 10),
                GenreId = Guid.NewGuid(),
                IsDeleted = false
            }
        };
    }
}