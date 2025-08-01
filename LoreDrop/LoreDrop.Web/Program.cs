using LoreDrop.Data;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Data.Seeder;
using LoreDrop.Data.Seeder.Interface;
using LoreDrop.Services.Core;
using LoreDrop.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace LoreDrop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException(
                                       "Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<LoreDropDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>() // Added for role support
                .AddEntityFrameworkStores<LoreDropDbContext>();
            
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddRazorPages();
            
            builder.Services.AddUserDefinedServices(typeof(SeriesService).Assembly);
            builder.Services.AddRepositories(typeof(ISeriesRepository).Assembly);
            
            // Register Identity Seeder
            builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();
            
            
            var app = builder.Build();
            

            // Rest of configuration...
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error/500"); // Handles 500 errors with custom view
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); // Handles 404, 403, etc.
                app.UseHsts();
            }

           

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.SeedDefaultIdentity();

            app.UseAuthentication();
            app.UseAuthorization();
            
            

            app.UserAdminRedirection();
            
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.MapRazorPages();

            app.Run();
        }
    }
}