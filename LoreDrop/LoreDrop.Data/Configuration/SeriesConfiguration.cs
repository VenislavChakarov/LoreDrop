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
    }
}