using AdSite.Data;
using AdSite.Data.Repositories;
using AdSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace AdSite.Helpers
{
    public static class StartupHelper
    {

        //custom services registration
        public static void RegisterApplicationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


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
            services.AddTransient<IAdService, AdService>();
            services.AddTransient<IAdRepository, AdRepository>();
            services.AddTransient<IWishlistService, WishlistService>();
            services.AddTransient<IWishlistRepository, WishlistRepository>();
            services.AddTransient<IUserRoleCountryRepository, UserRoleCountryRepository>();
            services.AddTransient<IUserRoleCountryService, UserRoleCountryService>();
        }

        public static void RegisterSupportedLanguages(IServiceCollection services, IConfiguration Configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            string defaultLanguage = Configuration["DefaultLanguage:Value"];

            var languageService = serviceProvider.GetService<ILanguageService>();
            var countryService = serviceProvider.GetService<ICountryService>();

            try
            {
                var countries = countryService.GetAll();
                if (countries != null)
                    foreach (var country in countries)
                    {
                        var languages = languageService.GetAll(country.ID);
                        List<CultureInfo> supportedCultures = new List<CultureInfo>();
                        foreach (var language in languages)
                        {
                            try
                            {
                                supportedCultures.Add(new CultureInfo(language.CultureId));
                            }
                            catch (Exception)
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
            }
            catch { }
        }

        public static RequestLocalizationOptions BuildLocalizationOptions(ILanguageService languageService,
            IConfiguration Configuration, Guid countryId)
        {
            try
            {
                string defaultLanguage = Configuration["DefaultLanguage:Value"];
                string countryPath = Configuration["DefaultLanguage:Value"];

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

        public static void MapSite(ILanguageService languageService,
            IConfiguration Configuration,
            IApplicationBuilder app,
            Guid countryId)
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseSession();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseRequestLocalization(
                BuildLocalizationOptions(languageService, Configuration, countryId)
                );
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
