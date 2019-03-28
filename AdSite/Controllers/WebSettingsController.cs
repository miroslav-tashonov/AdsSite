using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdSite.Data;
using AdSite.Models.DatabaseModels;
using AdSite.Services;
using AdSite.Models.CRUDModels;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace AdSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WebSettingsController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IWebSettingsService _webSettingsService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly string LOCALIZATION_ERROR_NOT_FOUND = "ErrorMessage_NotFound";
        private readonly string LOCALIZATION_ERROR_CONCURENT_EDIT = "ErrorMessage_ConcurrentEdit";

        public WebSettingsController(IWebSettingsService webSettingsService, ICountryService countryService, ILocalizationService localizationService, ILogger<WebSettingsController> logger)
        {
            _countryService = countryService;
            _webSettingsService = webSettingsService;
            _localizationService = localizationService;
            _logger = logger;
        }


        // GET: WebSettings/Details
        public async Task<IActionResult> Details()
        {
            Guid countryId = _countryService.Get();

            if (!WebSettingsExistsForCountry(countryId))
            {
                return RedirectToAction(nameof(Create));
            }

            var webSettings = _webSettingsService.GetWebSettingsForCountry(countryId);

            return View(webSettings);
        }
        
        // GET: WebSettings/Edit
        public async Task<IActionResult> Edit()
        {
            Guid countryId = _countryService.Get();

            if (!WebSettingsExistsForCountry(countryId))
            {
                return RedirectToAction(nameof(Create));
            }

            var webSettings = _webSettingsService.GetWebSettingsForCountry(countryId);

            return View(webSettings);
        }

        // GET: WebSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WebSettingsCreateModel webSettings)
        {
            if (ModelState.IsValid)
            {
                Guid countryId = _countryService.Get();

                try
                {
                    bool statusResult = _webSettingsService.CreateWebSettingsForCountry(webSettings, countryId);
                    if (statusResult)
                    {
                        return RedirectToAction(nameof(Details));
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return StatusCode(500, ex.Message);
                }
            }
            return View(webSettings);
        }

        // POST: WebSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WebSettingsEditModel webSettings)
        {
            if (ModelState.IsValid)
            {
                Guid countryId = _countryService.Get();

                try
                {
                    _webSettingsService.UpdateWebSettingsForCountry(webSettings, countryId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebSettingsExistsForCountry(countryId))
                    {
                        string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_NOT_FOUND, CultureId);
                        _logger.LogError(localizationKey);
                        return NotFound(localizationKey);
                    }
                    else
                    {
                        string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_CONCURENT_EDIT, CultureId);
                        _logger.LogError(localizationKey);
                        return NotFound(localizationKey);
                    }
                }
                return RedirectToAction(nameof(Details));
            }

            return View(webSettings);
        }
        

        private bool WebSettingsExistsForCountry(Guid countryId)
        {
            return _webSettingsService.WebSettingsExistForCountry(countryId);
        }
    }
}
