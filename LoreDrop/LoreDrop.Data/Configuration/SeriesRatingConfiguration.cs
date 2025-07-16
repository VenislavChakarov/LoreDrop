using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoreDrop.Data.Configuration;

public class SeriesRatingConfiguration : IEntityTypeConfiguration<SeriesRating>
{
    public void Configure(EntityTypeBuilder<SeriesRating> entity)
    {
        entity.HasKey(r => r.Id);
        
        entity
            .Property(r => r.Rating)
            .IsRequired();
        
        entity
            .Property(r => r.UserId)
            .IsRequired();
        
        entity.HasOne(r => r.Series)
            .WithMany(s => s.Ratings)
            .HasForeignKey(r => r.SeriesId);
        
        entity
            .HasQueryFilter(r=> !r.Series.IsDeleted );
    }
}

