using Autofac;
using BeymenCodeCase.Common;
using BeymenCodeCase.Context;
using BeymenCodeCase.IoC;
using BeymenCodeCase.Services.Configuration;
using BeymenCodeCase.Services.Helper;
using BeymenCodeCase.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeymenCodeCase.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ILifetimeScope AutofacContainer { get; private set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<StorageContext>(ops => ops.UseSqlServer(Configuration["ConnectionStrings:StorageContextConnection"]));

            // Initialize All Settings
            var connectionString = "localhost";
            var siteName = "application_name";
            var refreshTimerIntervalInMs = 10;
            // Initialize Settings
            var configurationReader = new ConfigurationReaderService(siteName, connectionString,
                refreshTimerIntervalInMs);
            // Initialize Settings Class
            var configuration = configurationReader.InitializeSettings();
            // Redis Injection
            var multiplexer = ConnectionMultiplexer.Connect(connectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddSingleton(configuration);
            // Background Task
            services.AddHostedService<BackgroundServiceTask>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.

            builder.RegisterModule(new AutofacIoC());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          


            ServiceHelper.Configure(app.ApplicationServices);
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


       
    }
}
