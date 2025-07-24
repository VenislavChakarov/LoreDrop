using LoreDrop.Data.Seeder.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LoreDrop.Data.Seeder
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private readonly string[] DefaultRoles 
            = new[] { "Admin", "User" };

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public IdentitySeeder(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            this.roleManager    = roleManager;
            this.userManager    = userManager;
            this.configuration  = configuration;
        }

        public async Task SeedIdentityAsync()
        {
            await SeedRolesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            foreach (var role in DefaultRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                        throw new Exception($"Error seeding role '{role}': {string.Join(", ", result.Errors.Select(e=>e.Description))}");
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            // pull credentials from appsettings.json
            var testEmail    = configuration["UserSeed:TestUser:Email"];
            var testPassword = configuration["UserSeed:TestUser:Password"];
            var adminEmail   = configuration["UserSeed:TestAdmin:Email"];
            var adminPassword= configuration["UserSeed:TestAdmin:Password"];

            if (new[] { testEmail, testPassword, adminEmail, adminPassword }.Any(string.IsNullOrWhiteSpace))
                throw new Exception("Missing TestUser/TestAdmin credentials in configuration.");

            // helper local to create+role
            async Task EnsureUser(string email, string password, string role)
            {
                var existing = await userManager.FindByEmailAsync(email);
                if (existing is null)
                {
                    var user = new IdentityUser { UserName = email, Email = email };
                    var createResult = await userManager.CreateAsync(user, password);
                    if (!createResult.Succeeded)
                        throw new Exception($"Error creating {role} '{email}': {string.Join(", ", createResult.Errors.Select(e=>e.Description))}");

                    var roleResult = await userManager.AddToRoleAsync(user, role);
                    if (!roleResult.Succeeded)
                        throw new Exception($"Error assigning {role} to '{email}': {string.Join(", ", roleResult.Errors.Select(e=>e.Description))}");
                }
            }

            await EnsureUser(testEmail, testPassword, "User");
            await EnsureUser(adminEmail, adminPassword, "Admin");
        }
    }
}
