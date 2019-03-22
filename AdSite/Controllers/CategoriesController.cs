using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdSite.Services;
using AdSite.Models.CRUDModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using AdSite.Mappers;
using System.Threading;

namespace AdSite.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILocalizationService _localizationService;
        ILogger<CategoriesController> _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private readonly string LOCALIZATION_ERROR_USER_MUST_LOGIN= "Categories_ErrorMessage_MustLogin";
        private readonly string LOCALIZATION_ERROR_CATEGORY_NOT_FOUND = "Categories_ErrorMessage_NotFound";
        private readonly string LOCALIZATION_ERROR_CATEGORY_CONCURENT_EDIT = "Categories_ErrorMessage_ConcurrentEdit";


        public CategoriesController(ICategoryService categoryService, ILocalizationService localizationService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _localizationService = localizationService;
            _logger = logger;
        }

        // GET: Categories/Details
        public IActionResult Details()
        {
            var viewModel = new CategoryFilterComponentViewModel();
            try
            {
                var categories = _categoryService.GetCategoryTree();
                var mappedJSTreeCategories = JSTreeViewModelMapper.MapToJSTreeViewModel(categories);
                viewModel = new CategoryFilterComponentViewModel { ComponentCategories = mappedJSTreeCategories };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message);
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
                    AuditedEntityMapper<CategoryCreateModel>.FillCreateAuditedEntityFields(entity, currentUser);

                    try
                    {
                        bool statusResult = _categoryService.Add(entity);
                        if (statusResult)
                        {
                            return Ok();
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
                            string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_CATEGORY_NOT_FOUND, CultureId);
                            _logger.LogError(localizationKey);
                            return NotFound(localizationKey);
                        }
                        else
                        {
                            string localizationKey = _localizationService.GetByKey(LOCALIZATION_ERROR_CATEGORY_CONCURENT_EDIT, CultureId);
                            _logger.LogError(localizationKey);
                            return NotFound(localizationKey);
                        }
                    }
                    return RedirectToAction(nameof(Details));
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


        // POST: Categories/Delete/5
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
                _categoryService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }

            return RedirectToAction(nameof(Details));
        }

        private bool CategoryExists(Guid id)
        {
            return _categoryService.Exists(id);
        }
    }
}
