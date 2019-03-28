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
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AdSite.Mappers;
using System.Threading;
using AdSite.Models.CRUDModels;

namespace AdSite.Controllers
{
    public class LocalizationsController : Controller
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly ICountryService _countryService;
        private readonly ILogger<LocalizationsController> _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly string LOCALIZATION_ERROR_USER_MUST_LOGIN = "ErrorMessage_MustLogin";
        private readonly string LOCALIZATION_ERROR_NOT_FOUND = "ErrorMessage_NotFound";
        private readonly string LOCALIZATION_ERROR_CONCURENT_EDIT = "ErrorMessage_ConcurrentEdit";

        public LocalizationsController(ILocalizationService localizationService, ICountryService countryService, ILanguageService languageService, ILogger<LocalizationsController> logger)
        {
            _localizationService = localizationService;
            _countryService = countryService;
            _languageService = languageService;
            _logger = logger;
        }
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                return View(_localizationService.GetLocalizationAsViewModel((Guid)id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }

        }

        public IActionResult Index()
        {
            try
            {
                Guid countryId = _countryService.Get();

                return View(_localizationService.GetAll(countryId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }

        }
        // GET: Localizations/Create
        public IActionResult Create()
        {
            try
            {
                Guid countryId = _countryService.Get();
                ViewBag.Languages = _languageService.GetAllAsLookup(countryId);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }
        }

        // POST: Localizations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]LocalizationCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                Guid countryId = _countryService.Get();
                AuditedEntityMapper<LocalizationCreateModel>.FillCountryEntityField(entity, countryId);

                try
                {
                    bool statusResult = _localizationService.Add(entity);
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
                string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_USER_MUST_LOGIN, CultureId);
                _logger.LogError(localizationKey);
                return NotFound(localizationKey);
            }
        }


        // GET: Localizations/Edit/Guid
        public IActionResult Edit(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            try
            {
                Guid countryId = _countryService.Get();
                ViewBag.Languages = _languageService.GetAllAsLookup(countryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
            }

            var localization = _localizationService.GetLocalizationAsEditModel((Guid)id);
            if (localization == null)
            {
                return NotFound();
            }
            return View(localization);
        }

        // POST: Localizations/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]LocalizationEditModel entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _localizationService.Update(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalizationExists(entity.Id))
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
                return RedirectToAction(nameof(Details), new { id = entity.Id });
            }
            else
            {
                string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_USER_MUST_LOGIN, CultureId);
                _logger.LogError(localizationKey);
                return NotFound(localizationKey);
            }
        }


        // GET: Localizations/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localization = _localizationService.GetLocalizationAsViewModel((Guid)id);
            if (localization == null)
            {
                return NotFound();
            }

            return View(localization);
        }


        // POST: Localizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                _localizationService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LocalizationExists(Guid id)
        {
            return _localizationService.Exists(id);
        }
    }
}
