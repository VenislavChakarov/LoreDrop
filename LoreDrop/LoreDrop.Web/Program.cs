using LoreDrop.Data;
using LoreDrop.Data.Models;
using LoreDrop.Data.Repository;
using LoreDrop.Data.Repository.Interfaces;
using LoreDrop.Services.Core;
using LoreDrop.Services.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoreDrop
{


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
                }).AddEntityFrameworkStores<LoreDropDbContext>()
                    .AddDefaultUI();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ISeriesService, SeriesService>();
            builder.Services.AddScoped<IHomeService, HomeServiece>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IDetailsService, DetailsService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IFavoriteService, FavoriteService>();
            builder.Services.AddScoped<IWatchListService, WatchListService>();
            
            builder.Services.AddScoped<SeriesRepsitory>();
            builder.Services.AddScoped<GenreRepository>();
            builder.Services.AddScoped<CommentsRepository>();
            builder.Services.AddScoped<UserFavoriteRepository>();
            builder.Services.AddScoped<UserWatchListRepository>();
            builder.Services.AddScoped<SeriesRatingRepository>();
            builder.Services.AddScoped<SeriesStateRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}