﻿using AdSite.Models;
using AdSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace AdSite.Controllers
{
    public class HomeController : Controller
    {
        string COUNTRY_ID = "CountryId";

        private readonly ILocalizationService _localizationService;
        private readonly IWebSettingsService _webSettingsService;
        private readonly ICountryService _countryService;
        private readonly ILogger<HomeController> _logger;

        private Guid CountryId => _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);

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
            return RedirectToAction("Index", "ManageAccount");
        }

        [Authorize(Roles = ("User"))]
        public IActionResult UsersPanel()
        {
            return RedirectToAction("MyAds", "Ads");
        }

        public IActionResult Contact()
        {
            return View("Contact");
        }

        [Authorize(Roles = ("User"))]
        public IActionResult Verifications()
        {
            return View("Verifications");
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
