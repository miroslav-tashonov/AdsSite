using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdSite.Models;
using Microsoft.AspNetCore.Authorization;
using AdSite.Services;

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
            return View();
        }
        [Authorize(Roles=("Admin"))]
        public IActionResult AdminPanel()
        {
            return View("_AdminNavigationPartial");
        }

        [Authorize(Roles = ("User"))]
        public IActionResult UsersPanel()
        {
            return View("_UsersNavigationPartial");
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
