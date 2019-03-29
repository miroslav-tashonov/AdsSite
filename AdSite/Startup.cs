using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdSite.Data;
using AdSite.Models;
using AdSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdSite.Data.Repositories;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using AdSite.Models.Extensions;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            RegisterApplicationServices(services);

            //needed for setting languages that are currently in DB
            var serviceProvider = services.BuildServiceProvider();
            RegisterSupportedLanguages(services, serviceProvider);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILanguageService languageService, ICountryService countryService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSession();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseRequestLocalization(BuildLocalizationOptions(languageService, countryService));

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        //custom services registration
        private void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ILocalizationService, LocalizationService>();
            services.AddTransient<ILocalizationRepository, LocalizationRepository>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IWebSettingsService, WebSettingsService>();
            services.AddTransient<IWebSettingsRepository, WebSettingsRepository>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICityRepository, CityRepository>();
        }

        private void RegisterSupportedLanguages(IServiceCollection services, ServiceProvider serviceProvider)
        {
            string defaultLanguage = Configuration["DefaultLanguage:Value"];
            var languageService = serviceProvider.GetService<ILanguageService>();
            var countryService = serviceProvider.GetService<ICountryService>();

            Guid countryId = countryService.Get();
            var languages = languageService.GetAll(countryId);

            List<CultureInfo> supportedCultures = new List<CultureInfo>();
            foreach (var language in languages)
            {
                try
                {
                    supportedCultures.Add(new CultureInfo(language.CultureId));
                }
                catch (Exception ex)
                {

                }
            }

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture: defaultLanguage, uiCulture: defaultLanguage);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        private RequestLocalizationOptions BuildLocalizationOptions(ILanguageService languageService, ICountryService countryService)
        {
            try
            {
                string defaultLanguage = Configuration["DefaultLanguage:Value"];
                Guid countryId = countryService.Get();
                var languages = languageService.GetAll(countryId);

                List<CultureInfo> supportedCultures = new List<CultureInfo>();
                foreach (var language in languages)
                {
                    supportedCultures.Add(new CultureInfo(language.CultureId));
                }

                var options = new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(defaultLanguage),
                    SupportedCultures = supportedCultures,
                    SupportedUICultures = supportedCultures
                };

                return options;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
