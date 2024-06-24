using Microsoft.AspNetCore.ResponseCompression;
using Mc2.CrudTest.BootStrap; // Assuming your BootStrap namespace
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Mc2.CrudTest.Presentation.Business;
using Mc2.CrudTest.Presentation.BusinessServiceContract;

namespace Mc2.CrudTest.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Connection to SQL
            var connectionString = configuration["ConnectionString"];
            BootStrap.bootstrap.WireUp(services, connectionString);

            
        }

        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");
        }
    }
}