using WebApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Startup
    {
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    string con = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;";
        //    services.AddDbContext<AdContext>(options => options.UseSqlServer(con));
        //    services.AddControllers(); 
        //}

        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseDefaultFiles();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}

        private readonly IWebHostEnvironment _currentEnvironment;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            _currentEnvironment = currentEnvironment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            AddDb(services);
            ConfigureDependencies(services);
        }

        public virtual void ConfigureDependencies(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddDb(IServiceCollection services)
        {
            if (_currentEnvironment.IsEnvironment("Testing"))
            {
                services.AddDbContextPool<AdContext>(options => options.UseInMemoryDatabase("TestingDB"));
            }
            else
            {
                string con = "Server=(localdb)\\mssqllocaldb;Database=adDB;Trusted_Connection=True;";
                services.AddDbContextPool<AdContext>(options => options.UseSqlServer(con));
            }
        }
    }
}