using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Views.Shared
{
    public static class AdminNavPages
    {
        public static string ActiveAdminPageKey => "ActiveAdminPage";

        public static string ManageUsers => "ManageUsers";
        public static string Categories => "Categories";
        public static string Cities => "Cities";
        public static string Countries => "Countries";
        public static string Languages => "Languages";
        public static string Localizations => "Localizations";
        public static string WebSettings => "WebSettings";

        public static string ManageUsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageUsers);
        public static string CategoriesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Categories);
        public static string CitiesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Cities);
        public static string CountriesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Countries);
        public static string LanguagesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Languages);
        public static string LocalizationsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Localizations);
        public static string WebSettingsNavClass(ViewContext viewContext) => PageNavClass(viewContext, WebSettings);


        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.TempData["ActiveAdminPage"]?.ToString();
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(ViewContext viewContext, string activePage) => viewContext.TempData[ActiveAdminPageKey] = activePage;
    }
}
