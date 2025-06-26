using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop.Data;

public class LoreDropDbContext : IdentityDbContext
{
    public LoreDropDbContext(DbContextOptions<LoreDropDbContext> options)
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}