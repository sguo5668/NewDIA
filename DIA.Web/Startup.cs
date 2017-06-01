using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;  //need it, otherwise, inmemory db does not work
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Localization;
using System.Globalization;

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using DIA.Entities;
using DIA.Services;


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


         services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
         services.AddSingleton<IStringLocalizerFactory, SingleFileResourceManagerStringLocalizerFactory>();



            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();
            services.AddMemoryCache();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<RequestLocalizationOptions>(
         opts =>
         {
             var supportedCultures = new List<CultureInfo>
             {

                        new CultureInfo("en-US"),

                        new CultureInfo("es-MX") ,

             };

             opts.DefaultRequestCulture = new RequestCulture("en-US");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
         });


            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IMemoryCache cache)
        {

            string _datekey = "referencetablevalue";
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");
            var startupLogger = loggerFactory.CreateLogger<Startup>();
            startupLogger.LogInformation($">{_datekey} generated and set in cache.");

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
            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

            var context = app.ApplicationServices.GetService<DIAContext>();
            AddTestData(context);
            app.UseMvc(
                
            //  routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Person}/{action=Index}/{id?}");
            //}
            
            );
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


            var testUser4 = new Person
            {

                Id = 1,
                DOB = Convert.ToDateTime("3/25/1997"),
                FirstName = "John",
                LastName = "Doe",
                ECN = "00023333",
                Email = "a@abc.com"
            };

            context.Persons.Add(testUser4);
            context.SaveChanges();


            var testUser5 = new PersonPhoneNumber
            {

                PersonID = 1,
                PhoneNumber = "111-222-3333",
                PhoneNumberTypeCode = 440,
                IsPrimary = true,
            };



            context.PersonPhoneNumbers.Add(testUser5);
            context.SaveChanges();



            var testUser5a = new PersonPhoneNumber
            {

                PersonID = 1,
                PhoneNumber = "124-222-3333",
                PhoneNumberTypeCode = 439,
                IsPrimary = false,
            };



            context.PersonPhoneNumbers.Add(testUser5a);
            context.SaveChanges();



            var testUser6 = new PersonAddress
            {

                PersonID = 1,
                AddressLine1 = "123 main st",
                City = "fremont",
                StateCode = 361,
                ZipCode = "94539",
                AddressTypeCode = 438

            };



            context.PersonAddresses.Add(testUser6);
            context.SaveChanges();



            var testUser7 = new PersonAddress
            {

                PersonID = 1,
                AddressLine1 = "456 main st",
                City = "sacramento",
                StateCode = 361,
                ZipCode = "93539",
                AddressTypeCode = 437
            };



            context.PersonAddresses.Add(testUser7);
            context.SaveChanges();






            var locale1 = new LocalizationRecord
            {

                
                Key = "abouttitle",
                ResourceKey = "PersonController",
                Text = "this is a test",
                LocalizationCulture = "en-US"
            };



            context.LocalizationRecords.Add(locale1);
            context.SaveChanges();



            var reftable = new ReferenceTable
            {

                TableName = "Claim_Type"
            };

            context.ReferenceTables.Add(reftable);

            context.SaveChanges();

            //int refid = reftable.Id;


            var reftablevalue = new ReferenceTableValue
            {

                TableName = "Claim_Type",
                Key = "Disability Claim"

            };

            context.ReferenceTableValues.Add(reftablevalue);

            var reftablevalue2 = new ReferenceTableValue
            {

                TableName = "Claim_Type",
                Key = "PFL Claim"

            };

            var reftablevalue3 = new ReferenceTableValue
            {

                TableName = "Job_Category",
                Key = "light",
              
                Text = "lightxxxxxxxxxxxxxxxxxx" 
              

            };

            var reftablevalue4 = new ReferenceTableValue
            {

                TableName = "Job_Category",
                Key = "medium",
                Text = "medium cccccccccccccccccccccccccccccc" 
           

            };


            var reftablevalue5 = new ReferenceTableValue
            {

                TableName = "Job_Category",
                Key = "heavy"

            };


            var reftablevalue6 = new ReferenceTableValue
            {

                TableName = "State_Code",
                Key = "CA",
                Id = 361

            };

            var reftablevalue7 = new ReferenceTableValue
            {

                TableName = "State_Code",
                Key = "NY",
                Id = 362
            };


            var reftablevalue8 = new ReferenceTableValue
            {

                TableName = "State_Code",
                Key = "Name",
                Text= "Name in Italien",
                ResourceKey = "PersonController",
                LocalizationCulture ="en-US"
            };


            ReferenceTableValue reftablevalue9 = new ReferenceTableValue()
            {
                TableName = "State_Code",
                Key = "Name",
                Text = "Name in english",
                ResourceKey = "PersonController",
                LocalizationCulture = "es-MX"

            };

            context.ReferenceTableValues.Add(reftablevalue2);
            context.ReferenceTableValues.Add(reftablevalue3);
            context.ReferenceTableValues.Add(reftablevalue4);
            context.ReferenceTableValues.Add(reftablevalue5);
            context.ReferenceTableValues.Add(reftablevalue6);
            context.ReferenceTableValues.Add(reftablevalue7);
            context.ReferenceTableValues.Add(reftablevalue8);
            context.ReferenceTableValues.Add(reftablevalue9);
            context.SaveChanges();



        }
    }
}

