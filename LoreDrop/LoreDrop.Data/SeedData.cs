using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace LoreDrop.Web.Data
{
    public static class SeedData
    {
        public static async Task EnsureAdminAsync(IServiceProvider services)
        {
            // pull in the required services
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var config      = services.GetRequiredService<IConfiguration>();

            // read admin settings from configuration (optional)
            string adminEmail    = config["AdminUser:Email"]    ?? "admin@example.com";
            string adminPassword = config["AdminUser:Password"] ?? "P@ssword123!";
            string adminRole     = config["AdminUser:Role"]     ?? "Admin";

            // 1. ensure the Admin role exists
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // 2. ensure the Admin user exists
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email    = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create Admin user: {string.Join(", ", result.Errors)}");
                }
            }

            // 3. ensure the Admin user is in the Admin role
            if (!await userManager.IsInRoleAsync(adminUser, adminRole))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
}
