using AdSite.Extensions;
using AdSite.Mappers;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace AdSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<CountriesController> _logger;

        private int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private const int SERVER_ERROR_CODE = 500;

        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_WARNING_INVALID_MODELSTATE => _localizationService.GetByKey("WarningMessage_ModelStateInvalid", CultureId);
        private string LOCALIZATION_ERROR_DEFAULT => _localizationService.GetByKey("ErrorMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_USER_MUST_LOGIN => _localizationService.GetByKey("ErrorMessage_MustLogin", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);
        private string LOCALIZATION_ERROR_CONCURENT_EDIT => _localizationService.GetByKey("ErrorMessage_ConcurrentEdit", CultureId);

        public CountriesController(ICountryService countryService, ILocalizationService localizationService, ILogger<CountriesController> logger)
        {
            _countryService = countryService;
            _localizationService = localizationService;
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
                return View(_countryService.GetCountryAsViewModel((Guid)id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
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
                return View(_countryService.GetCountries(columnName, searchString));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
            }

        }
        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]CountryCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    try
                    {
                        AuditedEntityMapper<CountryCreateModel>.FillCreateAuditedEntityFields(entity, currentUser);

                        bool statusResult = _countryService.Add(entity);
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
                        return StatusCode(SERVER_ERROR_CODE, ex.Message).WithError(ex.Message);
                    }
                }
                else
                {
                    _logger.LogError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                    return NotFound().WithError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                }
            }
            return View(entity).WithWarning(LOCALIZATION_WARNING_INVALID_MODELSTATE);
        }


        // GET: Citites/Edit/Guid
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            var city = _countryService.GetCountryAsEditModel((Guid)id);
            if (city == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            return View(city);
        }

        // POST: Cities/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]CountryEditModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    AuditedEntityMapper<CountryEditModel>.FillModifyAuditedEntityFields(entity, currentUser);

                    try
                    {
                        _countryService.Update(entity);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CountryExists(entity.ID))
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

                    return RedirectToAction(nameof(Details), new { id = entity.ID }).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                }
                else
                {
                    _logger.LogError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                    return NotFound().WithError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                }
            }
            return View(entity).WithWarning(LOCALIZATION_WARNING_INVALID_MODELSTATE);
        }


        // GET: Cities/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            var country = _countryService.GetCountryAsViewModel((Guid)id);
            if (country == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            return View(country);
        }


        // POST: Cities/Delete/5
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
                _countryService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(SERVER_ERROR_CODE).WithError(ex.Message);
            }

            return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
        }

        private bool CountryExists(Guid id)
        {
            return _countryService.Exist(id);
        }
    }
}
