using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdSite.Services;
using AdSite.Models.CRUDModels;
using Microsoft.Extensions.Logging;
using System.Threading;
using AdSite.Mappers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using AdSite.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AdSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguagesController : Controller
    {
        string COUNTRY_ID = "CountryId";

        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ICountryService _countryService;
        private readonly ILogger _logger;

        private Guid CountryId => _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);
        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly int SERVER_ERROR_CODE = 500;
        private readonly string ERROR_URL = "/Error/404";

        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_DEFAULT => _localizationService.GetByKey("ErrorMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);

        public LanguagesController(ILanguageService languageService, ILocalizationService localizationService, ICountryService countryService, ILogger<LanguagesController> logger)
        {
            _languageService = languageService;
            _localizationService = localizationService;
            _logger = logger;
            _countryService = countryService;
        }

        // GET: Languages
        public IActionResult Index(string columnName, string searchString)
        {
            searchString = String.IsNullOrEmpty(searchString) ? String.Empty : searchString;
            columnName = String.IsNullOrEmpty(columnName) ? String.Empty : columnName;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentColumn"] = columnName;

            try
            {
                return View(_languageService.GetAll(columnName, searchString, CountryId));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index)).WithError(ex.Message);
            }
        }

        // GET: Languages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]LanguageCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AuditedEntityMapper<LanguageCreateModel>.FillCountryEntityField(entity, CountryId);

                    bool statusResult = _languageService.Add(entity);
                    if (statusResult)
                    {
                        return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index)).WithError(LOCALIZATION_ERROR_DEFAULT);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return RedirectToAction(nameof(Index)).WithError(ex.Message);
                }

            }
            return View(entity);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                bool deleteResult = _languageService.Delete(id, CountryId);
                if (deleteResult)
                    return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                else
                {
                    _logger.LogError(LOCALIZATION_ERROR_NOT_FOUND);
                    return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index)).WithError(ex.Message);
            }
        }

        private bool LanguageExists(Guid id)
        {
            return _languageService.Exists(id);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            int lcid;
            Int32.TryParse(culture, out lcid);
            if (lcid > 0)
            {
                var cultureInfo = new CultureInfo(lcid);

                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureInfo.Name)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

                return LocalRedirect(returnUrl).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
            }
            else
                return LocalRedirect(ERROR_URL).WithError(LOCALIZATION_ERROR_DEFAULT);
        }
    }
}
