using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdSite.Data;
using AdSite.Models;
using AdSite.Services;
using Microsoft.AspNetCore.Http;
using AdSite.Helpers;
using AdSite.Extensions;

namespace AdSite
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
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(3600);
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            try
            {
                services.AddAuthentication()
                        .AddGoogle(googleOptions =>
                        {
                            googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                            googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                        })
                        .AddFacebook(facebookOptions =>
                        {
                            facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                            facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                        });
            }
            catch (Exception ex)
            {

            }

            #region Configure Users Access 
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 7;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            services.AddIdentity<ApplicationUser, ApplicationIdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc().AddNewtonsoftJson();

            StartupHelper.RegisterApplicationServices(services);
            //needed for setting languages that are currently in DB
            StartupHelper.RegisterSupportedLanguages(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILanguageService languageService, ICountryService countryService)
        {
            var countries = countryService.GetAll();
            if (countries == null || countries.Count == 0)
            {
                throw new Exception("Countries list is empty");
            }

            foreach (var country in countries)
            {
                Guid countryId = country.ID;
                app.Map("/" + country.Path,
                    app =>
                    {
                        app.UseMiddleware<CountryMiddleware>(countryId);

                        StartupHelper.MapSite(languageService, Configuration, app, countryId);
                    }
                );
            }
        }


    }
}
