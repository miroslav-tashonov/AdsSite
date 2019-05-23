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
using AdSite.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AdSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocalizationsController : Controller
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly ICountryService _countryService;
        private readonly ILogger<LocalizationsController> _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly int SERVER_ERROR_CODE = 500;
        private Guid CountryId => _countryService.Get();
        private string LOCALIZATION_ERROR_DEFAULT => _localizationService.GetByKey("ErrorMessage_Default", CultureId);
        private string LOCALIZATION_WARNING_INVALID_MODELSTATE => _localizationService.GetByKey("WarningMessage_ModelStateInvalid", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);
        private string LOCALIZATION_ERROR_USER_MUST_LOGIN => _localizationService.GetByKey("ErrorMessage_MustLogin", CultureId);
        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_CONCURENT_EDIT => _localizationService.GetByKey("ErrorMessage_ConcurrentEdit", CultureId);

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
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            try
            {
                return View(_localizationService.GetLocalizationAsViewModel((Guid)id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound().WithError(ex.Message);
            }

        }

        public IActionResult Index(string columnName, string searchString)
        {
            searchString = String.IsNullOrEmpty(searchString) ? String.Empty : searchString;
            columnName = String.IsNullOrEmpty(columnName) ? String.Empty : columnName;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentColumn"] = columnName;


            try
            {
                return View(_localizationService.GetAll(columnName, searchString, CountryId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound().WithError(ex.Message);
            }

        }
        // GET: Localizations/Create
        public IActionResult Create()
        {
            try
            {
                ViewBag.Languages = _languageService.GetAllAsLookup(CountryId);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound().WithError(ex.Message);
            }
        }

        // POST: Localizations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]LocalizationCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AuditedEntityMapper<LocalizationCreateModel>.FillCountryEntityField(entity, CountryId);

                    bool statusResult = _localizationService.Add(entity);
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
                    return StatusCode(SERVER_ERROR_CODE).WithError(ex.Message);
                }
            }

            ViewBag.Languages = _languageService.GetAllAsLookup(CountryId);
            return View(entity).WithWarning(LOCALIZATION_WARNING_INVALID_MODELSTATE);

        }


        // GET: Localizations/Edit/Guid
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                ViewBag.Languages = _languageService.GetAllAsLookup(CountryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound().WithError(ex.Message);
            }

            var localization = _localizationService.GetLocalizationAsEditModel((Guid)id);
            if (localization == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
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
                        _logger.LogError(LOCALIZATION_ERROR_NOT_FOUND);
                        return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                    }
                    else
                    {
                        _logger.LogError(LOCALIZATION_ERROR_CONCURENT_EDIT);
                        return NotFound().WithError(LOCALIZATION_ERROR_CONCURENT_EDIT);
                    }
                }
                return RedirectToAction(nameof(Details), new { id = entity.Id }).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
            }

            ViewBag.Languages = _languageService.GetAllAsLookup(CountryId);

            return View(entity).WithWarning(LOCALIZATION_WARNING_INVALID_MODELSTATE);
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
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
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
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            try
            {
                _localizationService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(SERVER_ERROR_CODE).WithError(ex.Message);
            }

            return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_ERROR_CONCURENT_EDIT);
        }

        private bool LocalizationExists(Guid id)
        {
            return _localizationService.Exists(id);
        }
    }
}
