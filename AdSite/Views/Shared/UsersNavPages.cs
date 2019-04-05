using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Views.Shared
{
    public static class UsersNavPages
    {
        public static string ActiveUserPageKey => "ActiveUserPage";

        public static string MyAds => "MyAds";
        public static string NewAd => "NewAd";

        public static string MyAdsNavClass(ViewContext viewContext) => PageNavClass(viewContext, MyAds);
        public static string NewAdNavClass(ViewContext viewContext) => PageNavClass(viewContext, NewAd);


        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.TempData["ActiveUserPage"]?.ToString();
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(ViewContext viewContext, string activePage) => viewContext.TempData[ActiveUserPageKey] = activePage;
    }
}
