using AdSite.Data;
using AdSite.Helpers;
using AdSite.Models;
using AdSite.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Text;

namespace AdSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddMemoryCache();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            try
            {
                services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration.GetValue<string>("EnvironmentUrl:Value"),
                            ValidAudience = Configuration.GetValue<string>("EnvironmentUrl:Value"),
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("SecretKey:Value")))
                        };
                    });/*.AddGoogle(googleOptions =>
                    {
                        googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                        googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    })
                    .AddFacebook(facebookOptions =>
                    {
                        facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                        facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    });*/
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
                        StartupHelper.MapSite(languageService, Configuration, app, countryId);

                        mapper.UseSpa(spa =>
                        {
                            spa.Options.SourcePath = "ClientApp";
                            spa.UseAngularCliServer(npmScript: "start-" + country.Path);
                        });
                    }
                );
            }


        }


    }
}
