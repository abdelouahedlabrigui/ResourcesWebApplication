using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ResourcesWebApplication.Models.Context;
using ResourcesWebApplication.Middlewares.Interfaces;
using ResourcesWebApplication.Middlewares.Providers;

namespace ResourcesWebApplication
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
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddSingleton<IRouteProvider, RouteProvider>();
            services.AddSession(options =>
            {
                // Set session timeout value (optional)
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5138")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOriginForGenAI",
                    builder => builder.WithOrigins("http://localhost:5148")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOriginForRequestsApp",
                    builder => builder.WithOrigins("http://localhost:5152")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOriginForResourcesWebApiNet9",
                    builder => builder.WithOrigins("http://localhost:5261")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOriginForMLReportingInAngularProject",
                    builder => builder.WithOrigins("https://localhost:44447")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowCredentials()
                        .WithExposedHeaders("Custom-Header")
                        .WithMethods("GET", "POST", "OPTIONS"));
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowSpecificOrigin");
            app.UseCors("AllowSpecificOriginForGenAI");
            app.UseCors("AllowSpecificOriginForRequestsApp");
            app.UseCors("AllowSpecificOriginForMLReportingInAngularProject");

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
