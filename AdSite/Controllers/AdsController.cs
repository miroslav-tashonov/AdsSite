using AdSite.Extensions;
using AdSite.Helpers;
using AdSite.Mappers;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AdSite.Controllers
{
    public class AdsController : Controller
    {
        private readonly IAdService _adService;
        private readonly ICityService _cityService;
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdsController> _logger;


        private int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private const int SERVER_ERROR_CODE = 500;
        private const int FIRST_PAGE_NUMBER = 1;
        private const int PAGE_SIZE = 3;
        private string CurrentUserId => _userManager.GetUserId(User);
        private Guid CountryId => _countryService.Get();


        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_WARNING_INVALID_MODELSTATE => _localizationService.GetByKey("WarningMessage_ModelStateInvalid", CultureId);
        private string LOCALIZATION_ERROR_DEFAULT => _localizationService.GetByKey("ErrorMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_USER_MUST_LOGIN => _localizationService.GetByKey("ErrorMessage_MustLogin", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);
        private string LOCALIZATION_ERROR_ONLY_OWNER_CAN_EDIT => _localizationService.GetByKey("ErrorMessage_OnlyOwnerCanEdit", CultureId);
        private string LOCALIZATION_ERROR_CONCURENT_EDIT => _localizationService.GetByKey("ErrorMessage_ConcurrentEdit", CultureId);

        public AdsController(IAdService adService, ICityService cityService, ICategoryService categoryService, ICountryService countryService, ILocalizationService localizationService, UserManager<ApplicationUser> userManager, ILogger<AdsController> logger)
        {
            _adService = adService;
            _cityService = cityService;
            _categoryService = categoryService;
            _countryService = countryService;
            _localizationService = localizationService;
            _userManager = userManager;
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
                return View(_adService.GetAdAsViewModel((Guid)id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
            }
        }

        public IActionResult Index(string columnName, string searchString, int? pageNumber, Guid? categoryId)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = FIRST_PAGE_NUMBER;
            }

            searchString = String.IsNullOrEmpty(searchString) ? String.Empty : searchString;
            columnName = String.IsNullOrEmpty(columnName) ? String.Empty : columnName;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentColumn"] = columnName;

            try
            {
                int numberOfThePage = pageNumber ?? FIRST_PAGE_NUMBER;
                int count;

                var pageModel = new PageModel()
                {
                    ColumnName = columnName,
                    SearchString = searchString,
                    CountryId = CountryId,
                    PageSize = PAGE_SIZE,
                    PageIndex = numberOfThePage
                };

                List<AdGridViewModel> items = new List<AdGridViewModel>();
                if (categoryId == null)
                {
                    items = _adService.GetPageForAdGrid(pageModel, out count);
                }
                else
                {
                    items = _adService.GetPageForAdGridByCategory(pageModel, (Guid)categoryId, out count);
                }

                return View(PaginatedList<AdGridViewModel>.CreatePageAsync(items, count, numberOfThePage, PAGE_SIZE));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
            }

        }


        public IActionResult Create()
        {
            try
            {
                ViewBag.Cities = _cityService.GetAllAsLookup(CountryId);
                ViewBag.Categories = _categoryService.GetAllAsLookup(CountryId);

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]AdCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    try
                    {
                        AuditedEntityMapper<AdCreateModel>.FillCreateAuditedEntityFields(entity, currentUser, CountryId);
                        entity.OwnerId = CurrentUserId;

                        #region File Map
                        //need to map files in controller because aspnet core assembly is present in project
                        if (entity.Files != null)
                        {
                            foreach (var file in entity.Files)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    file.CopyTo(memoryStream);
                                    entity.FilesAsListOfByteArray.Add(memoryStream.ToArray());
                                }
                            }
                        }
                        //
                        #endregion

                        bool statusResult = _adService.Add(entity);
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

            ViewBag.Cities = _cityService.GetAllAsLookup(CountryId);
            ViewBag.Categories = _categoryService.GetAllAsLookup(CountryId);
            return View(entity).WithWarning(LOCALIZATION_WARNING_INVALID_MODELSTATE);

        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            try
            {
                ViewBag.Cities = _cityService.GetAllAsLookup(CountryId);
                ViewBag.Categories = _categoryService.GetAllAsLookup(CountryId);

                var ad = _adService.GetAdAsEditModel((Guid)id);
                if (ad.OwnerId != CurrentUserId)
                {
                    return StatusCode(SERVER_ERROR_CODE).WithError(LOCALIZATION_ERROR_ONLY_OWNER_CAN_EDIT);
                }

                if (ad == null)
                {
                    return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                }

                return View(ad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound().WithError(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]AdEditModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    AuditedEntityMapper<AdEditModel>.FillModifyAuditedEntityFields(entity, currentUser);
                    entity.OwnerId = CurrentUserId;

                    try
                    {
                        _adService.Update(entity);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdExists(entity.ID))
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


            ViewBag.Cities = _cityService.GetAllAsLookup(CountryId);
            ViewBag.Categories = _categoryService.GetAllAsLookup(CountryId);
            return View(entity).WithWarning(LOCALIZATION_WARNING_INVALID_MODELSTATE);

        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            var ad = _adService.GetAdAsViewModel((Guid)id);
            if (ad == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            return View(ad);
        }


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
                _adService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(SERVER_ERROR_CODE).WithError(ex.Message);
            }

            return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
        }

        private bool AdExists(Guid id)
        {
            return _adService.Exists(id);
        }

        private int AdCount()
        {
            return _adService.Count();
        }
    }
}
