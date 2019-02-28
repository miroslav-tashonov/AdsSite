using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdSite.Models;
using Microsoft.AspNetCore.Authorization;
using AdSite.Services.LocalizationService;

namespace AdSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocalizationService _localizationService;
        public HomeController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public IActionResult Index()
        {
            string value = _localizationService.Get(new Guid(), "stringove");
            return View();
        }
        [Authorize(Roles=("Admin"))]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
