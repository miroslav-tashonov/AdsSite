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
            var value = _localizationService.Get(Guid.Parse("d469fb43-86a0-4784-a75d-009b9d0784a0"));
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
