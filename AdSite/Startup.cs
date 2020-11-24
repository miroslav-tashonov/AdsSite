using AdSite.Data;
using AdSite.Extensions;
using AdSite.Helpers;
using AdSite.Models;
using AdSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;

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
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

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
            catch (Exception)
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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            StartupHelper.RegisterApplicationServices(services, Configuration);
            //needed for setting languages that are currently in DB
            StartupHelper.RegisterSupportedLanguages(services, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILanguageService languageService, ICountryService countryService, IWebHostEnvironment env)
        {
            var countries = countryService.GetAll();
            if (countries == null || countries.Count == 0)
            {
                throw new Exception("Countries list is empty");
            }

            foreach (var country in countries)
            {
                app.UseRewriter(new RewriteOptions()
                            .AddRedirect(@"^$", "/" + country.Path, (int)HttpStatusCode.Redirect)
                            );

                Guid countryId = country.ID;
                app.Map("/" + country.Path,
                    mapper =>
                    {
                        app.UseMiddleware<CountryMiddleware>(countryId);
                        StartupHelper.MapSite(languageService, Configuration, app, countryId);

                        mapper.UseSpa(spa =>
                        {
                            spa.Options.SourcePath = "ClientApp";
                            spa.UseAngularCliServer(npmScript: "start");
                        });
                    }
                );

                
                //
            }


        }


    }
}
