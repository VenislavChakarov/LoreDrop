using System.Reflection;
using LoreDrop.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoreDrop.Data.Models;

namespace LoreDrop.Data;

public class LoreDropDbContext : IdentityDbContext
{
    public LoreDropDbContext(DbContextOptions<LoreDropDbContext> options)
        : base(options)
    {
        
    }
    
    public virtual DbSet<Content> Contents { get; set; }
    public virtual DbSet<Comments> Comments { get; set; } = null!;
    public virtual DbSet<UserSaved> UserSaved { get; set; } = null!;
    public virtual DbSet<UserFavorites> UserFavorites { get; set; } = null!;
    public virtual DbSet<Genre> Genres { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.ApplyConfiguration(new CommentsConfiguration());
    }
}