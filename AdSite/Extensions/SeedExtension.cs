using AdSite.Data;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Models.Extensions;
using AdSite.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public static Dictionary<string, string> localizations = new Dictionary<string, string>()
        {
            #region Localization Key, Values
            {"Country_Details","Details" },
            {"Country_Edit","Edit Country" },

            {"Country_Field_Name","Name" },
            {"Country_Field_Path","Path" },
            {"Country_Field_Abbreviation","Abbreviation" },
            {"Country_CreateNew","New Region/Country" },
            {"Countries_Entity","Countries" },
        {"General_Edit","Edit"},
        {"General_Details","Details"},
        {"General_Delete","Delete"},
        {"General_FindBy","Find By"},
        {"City_CreateNew","Create New City"},
        {"City_Edit","Edit City"},
        {"General_BackToList","Back to List"},
        {"City_Field_Name","Name"},
        {"City_Field_Postcode","Postcode"},
        {"General_Save","Save"},
        {"General_DeleteMessage","Are you sure you want to delete this?"},
        {"City_Entity","City"},
        {"Language_CreateNew","Create New Language"},
        {"Language_Fields_LanguageShortName","Language Short Name"},
        {"Language_Fields_LanguageName","Language Name"},
        {"Language_AddLanguage","Add Language"},
        {"WebSettings_Entity","Web Settings"},
        {"WebSettings_Fields_Phone","Phone"},
        {"WebSettings_Fields_Email","Email"},
        {"WebSettings_Fields_FacebookSocialLink","Facebook Link"},
        {"WebSettings_Fields_InstagramSocialLink","Instagram Link"},
        {"WebSettings_Fields_TwitterSocialLink","Twitter Link"},
        {"WebSettings_Fields_GooglePlusSocialLink","Google Plus Link"},
        {"WebSettings_Fields_VKSocialLink","VK Link"},
        {"Localization_CreateNew","Create New Localization"},
        {"Localization_Fields_LocalizationKey","Localization Key"},
        {"Localization_Fields_LocalizationValue","Localization Value"},
        {"Account_Entity","Account"},
        {"Account_Fields_Username","Username"},
        {"Account_Fields_Email","Email"},
        {"Account_CreateNew","Create New Account"},
        {"Account_ChangeRoleMessage","Are you sure you want to change role?"},
        {"Account_ChangeRole","Change Role"},
        {"Account_Fields_Roles","Roles"},
        {"Account_NewRole","New Role"},
        {"Ads_MyAds","My Ads"},
        {"Ads_NewAd","New Ad"},
        {"Ad_CreateNew","Create New Ad"},
        {"Ad_Edit","Edit Ad"},
        {"Ad_Field_Name","Name"},
        {"Ad_Field_Price","Price"},
        {"Ad_Fields_City","City"},
        {"Ad_Fields_Category","Category"},
        {"Ad_Fields_Upload","Upload one or more files using this form:"},
        {"Ad_Field_Description","Description"},
        {"Owner_Username","Username"},
        {"Owner_Email","Email"},
        {"Owner_Phone","Phone"},
        {"Wishlist_MyWishlist","My Wishlist"},
        {"General_Verifications","Verifications"},
        {"General_AddToWishlist","Add to Wishlist"},
        {"General_DeleteFromWishlist","Delete From Wishlist"},
        {"ErrorMessage_AlreadyExist","Already Exist"},
        {"Wishlist_Name","Wishlist"},
        {"Contacts_Feedback","Feedback"},
        {"Contacts_Location","534-540 Little Bourke St, Melbourne VIC 3000, Australia"},
        {"Ads_AllAds","All Ads"},
        {"Contacts_OurAddress","Our Address"},
        {"Contacts_Schedule","Schedule"},
        {"Contacts_Slogan","Let's have a talk together."},
        {"Contacts_ScheduleDefinition","Mon-Fri 07:00-22:00. Sat-Sun closed"},
        {"Contacts_Name","Name"},
        {"Contacts_Email","E-Mail"},
        {"Contacts_Message","Message"},
        {"Layouts_Information","Information"},
        {"Layouts_Location","Location"},
        {"Layouts_Address","Address"},
        {"Layouts_Contacts","Contact"},
        {"Layouts_Home","Home"},
        {"Layouts_Ads","Ads"},
        {"Layouts_AboutUs","About Us"},
        {"Layouts_Pages","Pages"},
        {"Layouts_Map","Map"},
        {"Layouts_Phone","Phone"},
        {"Layouts_SubscribeMessage","Subscribe to get news about ads"},
        {"Layouts_SubscribeEmailMessage","Enter your email subscribtion address here."},
        {"Wishlist_RemoveMessage","Remove from Wishlist"},
        {"General_Print","Print"},
        {"General_ShareOnFacebook","Share"},
        {"Ads_AdCreatedDate","Created Date"},
        {"Ads_RelatedAds","Related Ads"},
        {"Ads_OwnerName","Owner Name"},
        {"Ads_OwnerNumber","Owner Number"},
        {"Ads_OwnerMail","Owner Mail"},
        {"Ads_City","City"},
        {"Ads_Category","Category"},
        {"General_ButtonCreate","Create Now"},
        {"General_DiscountPercentage","DISCOUNT -30%"},
        {"General_DiscountMessage1","Account creation discount!"},
        {"General_DiscountMessage2","We all have choices for you. Check it out."},
        {"General_AdLeader","AD SITE LEADER"},
        {"General_AdLeaderMessage1","Leader in published ads in the region."},
        {"General_AdLeaderMessage2","Over 3000000 ads published only last year."},
        {"General_ButtonFindAd","Find Ad"},
        {"Category_Entity","Category"},
        {"Ad_SelectAttachment","Select Attachment"},
        {"LAYOUTS_CONTACTUS", "Contact Us" },
        {"LAYOUTS_ALLADS", "All Ads" },
        {"Layouts_Contact", "Contact" },
        {"LAYOUTS_ADMINPANEL", "Admin Panel" },
        {"ManageUsers_Entity", "Manage Users" },
        {"Localizations_Entity", "Localizations" },
        {"Languages_Entity", "Languages" },
        {"Cities_Entity", "Cities" },
        {"Categories_Entity", "Categories" },
        {"Categories_CreateCategoryStem", "Create Category" },
        {"Categories_CreateCategoryLeaf", "Create Subcategory" },
        {"Categories_EditCategory", "Edit Category" },
        {"Categories_DeselectAll", "Deselect All" },
        {"Categories_DeleteCategory", "Delete Category" },
        {"Layouts_MyProfile", "My Profile" },
        {"Language_Field_LanguageName", "Language Name" },
        {"WebSettings_CreateNew", "Web Settings" },
        {"LAYOUTS_USERPANEL", "User Panel" },
        {"General_Create", "Create" }
        #endregion
        };



        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SeedExtension>>();
            logger.LogInformation("creating database");

            using (var context = serviceProvider.GetService<ApplicationDbContext>())
            {
                context.Database.Migrate();
                context.Dispose();
            }
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SeedExtension>>();
            logger.LogInformation("adding customs roles");

            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationIdentityRole>>();

            string[] roleNames = {
                Enum.GetName(typeof(UserRole), UserRole.Admin),
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
                var countries = _countryService.GetAll();
                if (countries != null)
                    foreach (var country in countries)
                    {
                        if (_languageService.GetAll(country.ID).Count == 0)
                        {
                            LanguageCreateModel language = new LanguageCreateModel();
                            language.CountryId = country.ID;
                            language.CultureId = defaultCultureInfo.LCID.ToString();

                            _languageService.Add(language);
                        }
                    }
            }
            catch (Exception ex)
            {
                logger.LogInformation("Language cannot be added ." + ex.Message);
            }
        }

        public static async Task ImportLocalizations(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SeedExtension>>();
            logger.LogInformation("importing localizations start");

            try
            {
                var _countryService = serviceProvider.GetService<ICountryService>();
                var countries = _countryService.GetAll();
                if (countries != null)
                    foreach (var country in countries)
                    {
                        var _languageService = serviceProvider.GetService<ILanguageService>();
                        foreach (var countryLanguage in _languageService.GetAll(country.ID))
                        {

                            string defaultLanguage = configuration["DefaultLanguage:Value"];
                            CultureInfo defaultCultureInfo = new CultureInfo(defaultLanguage);
                            if (countryLanguage.CultureId == defaultCultureInfo.LCID)
                            {
                                var language = _languageService.GetByCultureId(defaultCultureInfo.LCID, country.ID);
                                var _localizationService = serviceProvider.GetService<ILocalizationService>();
                                var firstItemInDictionary = localizations.First();

                                if (!(_localizationService.GetByKey(firstItemInDictionary.Key, language.CultureId)
                                    == firstItemInDictionary.Value))
                                {
                                    foreach (KeyValuePair<string, string> localizationItem in localizations)
                                    {
                                        LocalizationCreateModel localization = new LocalizationCreateModel();
                                        localization.CountryId = country.ID;
                                        localization.LanguageId = language.ID;
                                        localization.LocalizationKey = localizationItem.Key;
                                        localization.LocalizationValue = localizationItem.Value;

                                        _localizationService.Add(localization);

                                    }
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                logger.LogInformation("Import localizations failed." + ex.Message);
                throw new Exception("Import localizations failed. " + ex.Message);
            }
        }

        public static async Task CreateDefaultCountry(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SeedExtension>>();
            logger.LogInformation("adding default country");

            try
            {
                var _countryService = serviceProvider.GetService<ICountryService>();
                if (!_countryService.GetAll().Any())
                {
                    string countryName = configuration["Country:Name"];
                    string countryPath = configuration["Country:Path"];
                    string countryAbbreviation = configuration["Country:Abbreviation"];

                    CountryCreateModel countryCreateModel = new CountryCreateModel
                    {
                        Name = countryName,
                        Path = countryPath,
                        Abbreviation = countryAbbreviation,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = DateTime.Now,
                        CreatedBy = "seedadmin",
                        ModifiedBy = "seedadmin",
                    };

                    if (!_countryService.Add(countryCreateModel))
                    {
                        logger.LogInformation("Country cannot be added .");
                        throw new Exception("Country cannot be added .");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("Country cannot be added ." + ex.Message);
                throw new Exception("Country cannot be added ." + ex.Message);
            }
        }


        //public static async Task CreateAdminAccount(IServiceProvider serviceProvider)
        //{
        //    var logger = serviceProvider.GetRequiredService<ILogger<SeedExtension>>();
        //    logger.LogInformation("adding default user");

        //    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        //    //todo hide credentials 
        //    if (!userManager.Users.Any(u => u.UserName == "admin@email.com"))
        //    {
        //        var user = new ApplicationUser { LockoutEnabled = false, EmailConfirmed = true, UserName = "admin@email.com", Email = "admin@email.com" };
        //        var result = await userManager.CreateAsync(user, "somepassword");
        //        if (result.Succeeded)
        //        {
        //            result = await userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRole), UserRole.Admin));
        //            if (result.Succeeded)
        //            {
        //                logger.LogInformation("Admin created !!");
        //            }
        //        }
        //    }
        //}
    }

}
