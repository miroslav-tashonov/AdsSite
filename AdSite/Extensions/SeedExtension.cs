using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using AdSite.Models.Extensions;
using AdSite.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Extensions
{
    public class SeedExtension
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SeedExtension>>();
            logger.LogInformation("adding customs roles");

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationIdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = {
                Enum.GetName(typeof(UserRole) ,UserRole.Admin),
                Enum.GetName(typeof(UserRole), UserRole.User),
                Enum.GetName(typeof(UserRole), UserRole.Viewer)
            };

            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                logger.LogInformation("creating the roles and seeding them to the database");
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new ApplicationIdentityRole(roleName));
                }
            }
        }
        

        public static async Task CreateDefaultLanguage(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SeedExtension>>();
            logger.LogInformation("adding default language");

            
            try
            {
                string defaultLanguage = configuration["DefaultLanguage:Value"];

                var _languageService = serviceProvider.GetService<ILanguageService>();
                var _countryService = serviceProvider.GetService<ICountryService>();
                CultureInfo defaultCultureInfo = new CultureInfo(defaultLanguage);
                Guid countryId = _countryService.Get();
                if (!_languageService.Exists(defaultCultureInfo.LCID, countryId))
                {
                    LanguageCreateModel language = new LanguageCreateModel();
                    language.CountryId = countryId;
                    language.CultureId = defaultCultureInfo.LCID.ToString();

                    _languageService.Add(language);

                }
            }
            catch(Exception ex)
            {
                logger.LogInformation("Language cannot be added ." + ex.Message);
                throw new Exception("Language cannot be added ." + ex.Message);
            }
        }
    }
}
