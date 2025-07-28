using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoreDrop.Data.Configuration;

public class UserWatchListConfiguration : IEntityTypeConfiguration<UserWatchList>
{
    public void Configure(EntityTypeBuilder<UserWatchList> entity)
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
            .WithMany(c => c.UserWathList)
            .HasForeignKey(us => us.SeriesId)
            .OnDelete(DeleteBehavior.Cascade);
        
        entity
            .HasOne(us => us.SeriesState)
            .WithMany()
            .HasForeignKey(us => us.SeriesStateId);
    }
}