using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LoreDrop.GCommon.ValidationConstants.Content;

namespace LoreDrop.Data.Configuration;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> entity)
    {
        entity
            .HasKey(c => c.Id);
        
        entity
            .Property(c => c.Tittle)
            .IsRequired()
            .HasMaxLength(TitleMaxLength);
        
        entity
            .Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);
        
        entity
            .Property(c => c.Rating)
            .IsRequired(false);
        
        entity
            .Property(c => c.ImageUrl)
            .IsRequired(false);
        
        entity
            .Property(c => c.CreatedOn)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);
        
        entity
            .Property(c => c.IsDeleted)
            .HasDefaultValue(false);
        
        entity
            .HasQueryFilter(c => c.IsDeleted == false);


        entity
            .HasOne(c => c.Genre)
            .WithMany(g => g.Contents)
            .HasForeignKey(c => c.GenreId);
    }
}