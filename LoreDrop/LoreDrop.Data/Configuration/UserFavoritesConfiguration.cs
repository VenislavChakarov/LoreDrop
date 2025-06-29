using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoreDrop.Data.Configuration;

public class UserFavoritesConfiguration : IEntityTypeConfiguration<UserFavorites>
{
    public void Configure(EntityTypeBuilder<UserFavorites> entity)
    {
        entity
            .HasKey(uf => new { uf.UserId, uf.ContentId });

        entity
            .HasQueryFilter(uf => uf.Content.IsDeleted == false);

        entity
            .HasOne(uf => uf.User)
            .WithMany()
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity
            .HasOne(uf => uf.Content)
            .WithMany(c => c.UserFavorites)
            .HasForeignKey(uf => uf.ContentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}