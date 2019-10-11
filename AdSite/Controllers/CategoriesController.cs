using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdSite.Services;
using AdSite.Models.CRUDModels;
using Microsoft.Extensions.Logging;
using AdSite.Mappers;
using System.Threading;
using AdSite.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AdSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        string COUNTRY_ID = "CountryId";

        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<CategoriesController> _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly int SERVER_ERROR_CODE = 500;
        private Guid CountryId => _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);

        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_USER_MUST_LOGIN => _localizationService.GetByKey("ErrorMessage_MustLogin", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);
        private string LOCALIZATION_ERROR_CONCURENT_EDIT => _localizationService.GetByKey("ErrorMessage_ConcurrentEdit", CultureId);

        public CategoriesController(ICategoryService categoryService, ILocalizationService localizationService, ILogger<CategoriesController> logger, ICountryService countryService)
        {
            _categoryService = categoryService;
            _localizationService = localizationService;
            _logger = logger;
            _countryService = countryService;
        }

        // GET: Categories/Details
        public IActionResult Details()
        {
            var viewModel = new CategoryTreeViewModel();
            try
            {
                var jstree = _categoryService.GetCategoriesAsJSTree(CountryId);
                viewModel = new CategoryTreeViewModel { CategoriesAsTree = jstree };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound().WithError(ex.Message);
            }

            return View(viewModel);
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody]CategoryCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    try
                    {
                        AuditedEntityMapper<CategoryCreateModel>.FillCreateAuditedEntityFields(entity, currentUser, CountryId);

                        bool statusResult = _categoryService.Add(entity);
                        if (statusResult)
                        {
                            return Ok().WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                        }
                        else
                        {
                            return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        return RedirectToAction(nameof(Index)).WithError(ex.Message);
                    }
                }
                else
                {
                    _logger.LogError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                    return NotFound().WithError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                }
            }

            return View(entity);
        }

        // POST: Categories/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody]CategoryEditModel entity)
        {
            if (ModelState.IsValid)
            {
                string currentUser = HttpContext?.User?.Identity?.Name;
                if (!String.IsNullOrEmpty(currentUser))
                {
                    AuditedEntityMapper<CategoryEditModel>.FillModifyAuditedEntityFields(entity, currentUser);

                    try
                    {
                        _categoryService.Update(entity);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoryExists(entity.ID))
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
                    return Ok().WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                }
                else
                {
                    _logger.LogError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                    return NotFound().WithError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                }
            }
            return View(entity);
        }


        // POST: Categories/Delete/5
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
                _categoryService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RedirectToAction(nameof(Index)).WithError(ex.Message);
            }

            return Ok().WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
        }

        private bool CategoryExists(Guid id)
        {
            return _categoryService.Exists(id);
        }
    }
}
