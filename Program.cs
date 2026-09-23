using CampingRohani.Data;
using Microsoft.EntityFrameworkCore;
using CampingRohani.Data;

namespace CampingRohani
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            var conn = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CampingRohaniContext>(options =>
                options.UseMySql(conn, ServerVersion.AutoDetect(conn))
            );

            var app = builder.Build();

            // Seed database on startup
            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<CampingRohaniContext>();
            //    var seeder = new DatabaseSeeder(context);
            //    seeder.SeedAsync().Wait();
            //}

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
