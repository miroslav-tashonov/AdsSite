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

namespace AdSite.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ICountryService _countryService;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly string LOCALIZATION_ERROR_NOT_FOUND = "ErrorMessage_NotFound";

        public LanguagesController(ILanguageService languageService, ILocalizationService localizationService, ICountryService countryService, ILogger<LanguagesController> logger)
        {
            _languageService = languageService;
            _localizationService = localizationService;
            _logger = logger;
            _countryService = countryService;
        }

        // GET: Languages
        public IActionResult Index()
        {
            try
            {
                Guid countryId = _countryService.Get();

                return View(_languageService.GetAll(countryId));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                Guid countryId = _countryService.Get();
                AuditedEntityMapper<LanguageCreateModel>.FillCountryEntityField(entity, countryId);

                try
                {
                    bool statusResult = _languageService.Add(entity);
                    if (statusResult)
                    {
                        return RedirectToAction(nameof(Index));
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
            else
            {
                return StatusCode(500);
            }
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                Guid countryId = _countryService.Get();

                bool deleteResult = _languageService.Delete(id, countryId);
                if (deleteResult)
                    return RedirectToAction(nameof(Index));
                else
                {
                    string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_NOT_FOUND, CultureId);
                    _logger.LogError(localizationKey);
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private bool LanguageExists(Guid id)
        {
            return _languageService.Exists(id);
        }

        [HttpPost]
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

                return LocalRedirect(returnUrl);
            }
            else
                return LocalRedirect("/Error/404");
        }
    }
}
