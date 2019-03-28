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
using Microsoft.Extensions.Logging;
using System.Threading;
using AdSite.Mappers;
using AdSite.Models.CRUDModels;

namespace AdSite.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<CitiesController> _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly string LOCALIZATION_ERROR_USER_MUST_LOGIN = "ErrorMessage_MustLogin";
        private readonly string LOCALIZATION_ERROR_NOT_FOUND = "ErrorMessage_NotFound";
        private readonly string LOCALIZATION_ERROR_CONCURENT_EDIT = "ErrorMessage_ConcurrentEdit";

        public CitiesController(ICityService cityService, ICountryService countryService,ILocalizationService localizationService,ILogger<CitiesController> logger)
        {
            _cityService = cityService;
            _countryService = countryService;
            _localizationService = localizationService;
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
                return View(_cityService.GetCityAsViewModel((Guid)id));
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

                return View(_cityService.GetCities(countryId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
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
        public IActionResult Create([FromForm]CityCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    Guid countryId = _countryService.Get();
                    AuditedEntityMapper<CityCreateModel>.FillCreateAuditedEntityFields(entity, currentUser, countryId);

                    try
                    {
                        bool statusResult = _cityService.Add(entity);
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
            else
            {
                return StatusCode(500);
            }
        }


        // GET: Citites/Edit/Guid
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetCityAsEditModel((Guid)id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody]CityEditModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    AuditedEntityMapper<CityEditModel>.FillModifyAuditedEntityFields(entity, currentUser);

                    try
                    {
                        _cityService.Update(entity);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CityExists(entity.ID))
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
                    return RedirectToAction(nameof(Details), new { id = entity.ID });
                }
                else
                {
                    string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_USER_MUST_LOGIN, CultureId);
                    _logger.LogError(localizationKey);
                    return NotFound(localizationKey);
                }
            }
            return View(entity);
        }


        // GET: Cities/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetCityAsViewModel((Guid)id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        // POST: Cities/Delete/5
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
                _cityService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(Guid id)
        {
            return _cityService.Exists(id);
        }
    }
}
