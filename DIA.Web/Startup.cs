using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DIA.Services;
using DIA.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DIA.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<DIAContext>(opt => opt.UseInMemoryDatabase());
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            var context = app.ApplicationServices.GetService<DIAContext>();
            AddTestData(context);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Claim}/{action=Index}/{id?}");
            });
        }


        private static void AddTestData(DIAContext context)
        {
            var testUser1 = new Claim
            {
                ClaimIDNumber = "DI1000000001",
                ClaimTypeCode = 1639,
                ClaimantID = 1,

                EffectiveDate = Convert.ToDateTime("1/25/2017")
            };
            context.Claims.Add(testUser1);

            var testUser2 = new Claim
            {
                ClaimIDNumber = "DI1000000002",
                ClaimTypeCode = 1639,
                ClaimantID = 2,

                EffectiveDate = Convert.ToDateTime("2/25/2017")
            };
            context.Claims.Add(testUser2);

            var testUser3 = new Claim
            {
                ClaimIDNumber = "DI1000000003",
                ClaimTypeCode = 1639,
                ClaimantID = 2,
                Id = 4,
                EffectiveDate = Convert.ToDateTime("3/25/2017")
            };
            context.Claims.Add(testUser3);




            context.SaveChanges();

        }
    }
}

