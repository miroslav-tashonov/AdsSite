using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdSite.Models;
using Microsoft.AspNetCore.Authorization;
using AdSite.Services;
using Microsoft.Extensions.Logging;

namespace AdSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocalizationService _localizationService;
        private readonly IWebSettingsService _webSettingsService;
        private readonly ICountryService _countryService;
        private readonly ILogger<HomeController> _logger;

        private Guid CountryId => _countryService.Get();

        public HomeController(ILocalizationService localizationService, IWebSettingsService webSettingsService, ICountryService countryService, ILogger<HomeController> logger)
        {
            _localizationService = localizationService;
            _webSettingsService = webSettingsService;
            _countryService = countryService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = ("Admin"))]
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
            return View("Contact");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
