using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoreDrop.Data.Configuration;

public class UserSavedConfiguration : IEntityTypeConfiguration<UserSaved>
{
    public void Configure(EntityTypeBuilder<UserSaved> entity)
    {
        entity
            .HasKey(us => new { us.UserId, us.SeriesId });
        
        entity
            .HasQueryFilter(us => us.Series.IsDeleted == false);

        entity
            .HasOne(us => us.User)
            .WithMany()
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity
            .HasOne(us => us.Series)
            .WithMany(c => c.UserSaved)
            .HasForeignKey(us => us.SeriesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}