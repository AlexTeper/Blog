using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityCheck.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityCheck.Models;
using IdentityCheck.Services;
using IdentityCheck.Services.User;
using AutoMapper;
using IdentityCheck.Services.Helpers.AutoMapper.Profiles;
using ReflectionIT.Mvc.Paging;
using IdentityCheck.Configs;
using Microsoft.AspNetCore.Mvc.Razor;
using IdentityCheck.Resources;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace IdentityCheck
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
            });
            services.SetLocalizationSource();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.SetUpAutoMapper();
            services.AddPaging();

            // We store out google credentails in secrets with the help of our package manager console
            // dotnet user-secrets set "Movies:ServiceApiKey" "12345"
            // the SecretManager will create a new secrets.json for your project and stores it on
            // your computer. Look it up :)
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });

            services.AddMvc()
               .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
               .AddDataAnnotationsLocalization(o =>
               {
                   var type = typeof(SharedResources);
                   var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                   var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                   var localizer = factory.Create("SharedResources", assemblyName.Name);
                   // for translating error messages
                   o.DataAnnotationLocalizerProvider = (t, f) => localizer;
               });
               
            services.SetLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationContext)
        {
            ApplicationDbInitializer.SeedUsers(userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //applicationContext.Database.Migrate();  // database migration to remote server
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRequestLocalization();
            app.UseMvc();
        }
    }
}
