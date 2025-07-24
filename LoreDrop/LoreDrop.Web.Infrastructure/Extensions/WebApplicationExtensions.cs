
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using LoreDrop.Data.Seeder.Interface;
using LoreDrop.Web.Infrastructure.Middlewares;

namespace LoreDrop.Web.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UserAdminRedirection(this WebApplication app)
    {
        app.UseMiddleware<AdminRedirectionMiddleware>();

        return app;
    }

    public static WebApplication SeedDefaultIdentity(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IIdentitySeeder>();
        seeder.SeedIdentityAsync().GetAwaiter().GetResult();
        return app;
    }
}