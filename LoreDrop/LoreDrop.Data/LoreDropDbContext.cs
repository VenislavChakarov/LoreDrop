using System.Reflection;
using LoreDrop.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoreDrop.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace LoreDrop.Data;

public class LoreDropDbContext : IdentityDbContext<IdentityUser>
{
    public LoreDropDbContext(DbContextOptions<LoreDropDbContext> options)
        : base(options)
    {
        
    }
    
    public virtual DbSet<Series> Series { get; set; } = null!;
    public virtual DbSet<Comments> Comments { get; set; } = null!;
    public virtual DbSet<UserWatchList> UserWatchLists { get; set; } = null!;
    public virtual DbSet<UserFavorites> UserFavorites { get; set; } = null!;
    public virtual DbSet<Genre> Genres { get; set; } = null!;
    public virtual DbSet<SeriesState> SeriesStates { get; set; } = null!;
    public virtual DbSet<SeriesRating> SeriesRatings { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}