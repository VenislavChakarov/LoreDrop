using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoreDrop.Data.Configuration;

public class UserFavoritesConfiguration : IEntityTypeConfiguration<UserFavorites>
{
    public void Configure(EntityTypeBuilder<UserFavorites> entity)
    {
        entity
            .HasKey(uf => new { uf.UserId, uf.SeriesId });

        entity
            .HasQueryFilter(uf => uf.Series.IsDeleted == false);

        entity
            .HasOne(uf => uf.User)
            .WithMany()
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity
            .HasOne(uf => uf.Series)
            .WithMany(c => c.UserFavorites)
            .HasForeignKey(uf => uf.SeriesId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}