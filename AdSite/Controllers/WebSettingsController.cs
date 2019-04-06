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
using AdSite.Extensions;

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
        private readonly int SERVER_ERROR_CODE = 500;

        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);

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
                try
                {
                    Guid countryId = _countryService.Get();
                
                    bool statusResult = _webSettingsService.CreateWebSettingsForCountry(webSettings, countryId);
                    if (statusResult)
                    {
                        return RedirectToAction(nameof(Details)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                    }
                    else
                    {
                        return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return StatusCode(SERVER_ERROR_CODE).WithError(ex.Message);
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
                        _logger.LogError(LOCALIZATION_ERROR_NOT_FOUND);
                        return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                    }
                    else
                    {
                        _logger.LogError(LOCALIZATION_ERROR_NOT_FOUND);
                        return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                    }
                }
                return RedirectToAction(nameof(Details)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
            }

            return View(webSettings);
        }
        

        private bool WebSettingsExistsForCountry(Guid countryId)
        {
            return _webSettingsService.WebSettingsExistForCountry(countryId);
        }
    }
}
