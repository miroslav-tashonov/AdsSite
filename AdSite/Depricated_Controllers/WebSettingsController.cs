using AdSite.Extensions;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AdSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WebSettingsController : Controller
    {
        string COUNTRY_ID = "CountryId";

        private readonly ICountryService _countryService;
        private readonly IWebSettingsService _webSettingsService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;

        private Guid CountryId => _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);
        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;

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
            if (!WebSettingsExistsForCountry(CountryId))
            {
                return RedirectToAction(nameof(Create));
            }

            var webSettings = _webSettingsService.GetWebSettingsViewModelForCountry(CountryId);

            return View(webSettings);
        }

        // GET: WebSettings/Edit
        public async Task<IActionResult> Edit()
        {
            if (!WebSettingsExistsForCountry(CountryId))
            {
                return RedirectToAction(nameof(Create));
            }

            var webSettings = _webSettingsService.GetWebSettingsEditModelForCountry(CountryId);

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
                    bool statusResult = _webSettingsService.CreateWebSettingsForCountry(webSettings, CountryId);
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
                    return RedirectToAction(nameof(Details)).WithError(ex.Message);
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
                try
                {
                    _webSettingsService.UpdateWebSettingsForCountry(webSettings, CountryId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebSettingsExistsForCountry(CountryId))
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
