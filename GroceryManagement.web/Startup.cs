using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using GroceryManagement.web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

// NOTE: add the Nuget Package "Swashbuckle.AspNetCore"
// to enable Swagger Documentation Generation for OpenAPI documentation.

// Add the assembly attribute, to ensure that the Swagger generates the complete API Documentation.
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace GroceryManagement.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services
            //    .AddDbContext<ApplicationDbContext>((options) =>
            //    {
            //        options.UseSqlServer(Configuration.GetConnectionString("MyDefaultConnectionString"));
            //    });

            services
             .AddDbContext<ApplicationDbContext>((options) =>
             {
                 options.UseSqlServer(Configuration.GetConnectionString("MyDefaultConnectionString"));
             });

            // Register the OWIN Identity Middleware
            // to use the default IdentityUser and IdentityRole profiles
            // and store the data in the ApplicationDbContext
            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddRazorPages();
            // Register the MVC Middleware - NEEDED for Swagger Documentation Middleware 
            services.AddMvc();

            // Register the Swagger Documentation Generation Middleware Service
            // URL: https://localhost:xxxx/swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Grocery Store ",
                    Description = "Grocery Management System - API version 1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Add the Swagger Middleware
                app.UseSwagger();

                // Add the Swagger Documentation Generation Middleware
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Grocery Web API v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Register the MVC Middleware
            // - NEEDED for Swagger Documentation Middleware 
            // - NEEDED for the API support (if applicable)
            //services.AddMvc();

            // Activate the OWIN Middleware to use Authentication and Authorization Services.
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                // Register the ASP.NET Routes for Areas
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area}/{controller}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
