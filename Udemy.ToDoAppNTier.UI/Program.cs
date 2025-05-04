using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Udemy.ToDoAppNTier.Business.DependencyResolvers.Microsoft;
using Udemy.ToDoAppNTier.DataAccess.Context;

namespace Udemy.ToDoAppNTier.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register services
            builder.Services.AddDbContext<ToDoContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDependencies();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the application
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?code={0}");

            app.UseStaticFiles(); // Enable static file serving

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/node_modules"
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}
