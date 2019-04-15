﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisChaeGrowthplan.Controllers;
using RedisChaeGrowthplan.Models;
using RedisChaeGrowthplan.Common;
using RedisChaeGrowthplan.Stores;
using Microsoft.EntityFrameworkCore;

namespace RedisChaeGrowthplan
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

            services.AddMemoryCache();

            ///注入实例化 
            services.AddSingleton(typeof(IRedisCache), new RedisCache(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions
            {
                Configuration = Configuration.GetSection("Cache:RedisConnectionString").Value,
                InstanceName = Configuration.GetSection("Cache:InstanceName").Value
            }));
           

            services.AddDbContext<AprilDbContext>(option  => {
                option.UseMySQL(Configuration.GetSection("ConnectionStrings:MysqlConnection").Value);
            });

          

            services.AddScoped<HomeController>();
            services.AddScoped<LoginController>();                 
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
           

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}

