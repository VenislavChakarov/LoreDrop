using LoreDrop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static LoreDrop.GCommon.ValidationConstants.Comments;

namespace LoreDrop.Data.Configuration;

public class CommentsConfiguration : IEntityTypeConfiguration<Comments>
{
    public void Configure(EntityTypeBuilder<Comments> entity)
    {
        entity.HasKey(c => c.Id);

        entity.Property(c => c.Text)
            .IsRequired()
            .HasMaxLength(TextMaxLength);

        entity.Property(c => c.CreatedOn)
            .IsRequired()
            .HasDefaultValue(DateTime.UtcNow);

        entity.HasOne(c => c.Content)
            .WithMany(content => content.Comments)
            .HasForeignKey(c => c.ContentId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}