using AdSite.Extensions;
using AdSite.Helpers;
using AdSite.Mappers;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdSite.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class AdsController : Controller
    {
        string COUNTRY_ID = "CountryId";

        private readonly IAdService _adService;
        private readonly ICityService _cityService;
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly IWishlistService _wishlistService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdsController> _logger;


        private int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private const int SERVER_ERROR_CODE = 500;
        private const int FIRST_PAGE_NUMBER = 1;
        private const int DEFAULT_PAGE_SIZE = 10;
        private string DEFAULT_SEARCH_COLUMN_NAME => "Name";
        private string DEFAULT_SORT_COLUMN_NAME => "Cheapest";
        private const int PAGE_SIZE = 3;
        private string CurrentUserId => _userManager.GetUserId(User);
        private Guid CountryId => _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);


        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_WARNING_INVALID_MODELSTATE => _localizationService.GetByKey("WarningMessage_ModelStateInvalid", CultureId);
        private string LOCALIZATION_ERROR_DEFAULT => _localizationService.GetByKey("ErrorMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_USER_MUST_LOGIN => _localizationService.GetByKey("ErrorMessage_MustLogin", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);
        private string LOCALIZATION_ERROR_ONLY_OWNER_CAN_EDIT => _localizationService.GetByKey("ErrorMessage_OnlyOwnerCanEdit", CultureId);
        private string LOCALIZATION_ERROR_CONCURENT_EDIT => _localizationService.GetByKey("ErrorMessage_ConcurrentEdit", CultureId);


        public AdsController(IAdService adService, ICityService cityService, IWishlistService wishlistService, ICategoryService categoryService, ICountryService countryService, ILocalizationService localizationService, UserManager<ApplicationUser> userManager, ILogger<AdsController> logger)
        {
            _wishlistService = wishlistService;
            _adService = adService;
            _cityService = cityService;
            _categoryService = categoryService;
            _countryService = countryService;
            _localizationService = localizationService;
            _userManager = userManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            try
            {
                var viewModel = _adService.GetAdAsViewModel((Guid)id);
                //viewModel.IsInWishlist = _wishlistService.IsInWishlist((Guid)id, CurrentUserId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
            }
        }

        [AllowAnonymous]
        public IActionResult Index(PageQueryModel queryModel)
        {
            if (!String.IsNullOrEmpty(queryModel.SearchString))
            {
                queryModel.PageNumber = FIRST_PAGE_NUMBER;
            }

            queryModel.CategoryId = (queryModel.CategoryId == Guid.Empty || queryModel.CategoryId == null) ? null : queryModel.CategoryId;
            queryModel.SearchString = String.IsNullOrEmpty(queryModel.SearchString) ? String.Empty : queryModel.SearchString;
            queryModel.ColumnName = String.IsNullOrEmpty(queryModel.ColumnName) ? DEFAULT_SEARCH_COLUMN_NAME : queryModel.ColumnName;
            queryModel.SortColumn = String.IsNullOrEmpty(queryModel.SortColumn) ? DEFAULT_SORT_COLUMN_NAME : queryModel.SortColumn;

            try
            {
                int numberOfThePage = queryModel.PageNumber ?? FIRST_PAGE_NUMBER;
                int defaultPageSize = queryModel.PageSize ?? DEFAULT_PAGE_SIZE;
                int count, maxPrice;

                var pageModel = new PageModel()
                {
                    ColumnName = queryModel.ColumnName,
                    SearchString = queryModel.SearchString,
                    SortColumn = queryModel.SortColumn,
                    CountryId = CountryId,
                    PageSize = defaultPageSize,
                    PageIndex = numberOfThePage,
                    CurrentUser = CurrentUserId
                };

                var filterModel = new FilterModel()
                {
                    CategoryId = queryModel.CategoryId,
                    CityIds = queryModel.CityIds,
                    MinimumPrice = queryModel.MinPriceValue,
                    MaximumPrice = queryModel.MaxPriceValue
                };

                List<AdGridViewModel> items = _adService.GetPageForAdGridByFilter(pageModel, filterModel, out count, out maxPrice);

                ViewData["MinimumPrice"] = queryModel.MinPriceValue;
                ViewData["MaximumPrice"] = queryModel.MaxPriceValue == 0 ? maxPrice : queryModel.MaxPriceValue;
                ViewData["CityIds"] = queryModel.CityIds;
                ViewData["CategoryId"] = queryModel.CategoryId;
                ViewData["CurrentFilter"] = queryModel.SearchString;
                ViewData["CurrentColumn"] = queryModel.ColumnName;
                ViewData["SortColumn"] = queryModel.SortColumn;
                ViewData["PageSize"] = queryModel.PageSize;

                return View(PaginatedList<AdGridViewModel>.CreatePageAsync(items, count, numberOfThePage, defaultPageSize, maxPrice));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
            }
        }


        public IActionResult MyAds(string columnName, string searchString, string sortColumn, int? pageNumber)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = FIRST_PAGE_NUMBER;
            }

            searchString = String.IsNullOrEmpty(searchString) ? String.Empty : searchString;
            columnName = String.IsNullOrEmpty(columnName) ? DEFAULT_SEARCH_COLUMN_NAME : columnName;
            sortColumn = String.IsNullOrEmpty(sortColumn) ? DEFAULT_SORT_COLUMN_NAME : sortColumn;
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentColumn"] = columnName;
            ViewData["SortColumn"] = sortColumn;

            try
            {
                int numberOfThePage = pageNumber ?? FIRST_PAGE_NUMBER;
                int count;

                var pageModel = new PageModel()
                {
                    ColumnName = columnName,
                    SearchString = searchString,
                    SortColumn = sortColumn,
                    CountryId = CountryId,
                    PageSize = PAGE_SIZE,
                    PageIndex = numberOfThePage,
                    CurrentUser = CurrentUserId
                };

                var items = _adService.GetPageForMyAdsGrid(pageModel, CurrentUserId, out count);

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
                        string[] result;
                        if (!String.IsNullOrEmpty(entity.SerializedAdDetailsPictures))
                        {
                            result = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(entity.SerializedAdDetailsPictures);
                            entity.MainPictureFile = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(entity.MainPictureFile);
                            EntityFormMap(result, entity);
                        }
                        else
                        {
                            MapFiles(entity);
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
                        return RedirectToAction(nameof(Index)).WithError(ex.Message);
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

            #region Preserve pictures if model validation fails 
            string serializedString = String.Empty;
            string mainPictureSerializedString = string.Empty;
            if (entity.Files != null && entity.Files.Count() > 0)
            {
                MapFiles(entity);
                serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray);
                mainPictureSerializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray.First());
            }
            if (!String.IsNullOrEmpty(entity.SerializedAdDetailsPictures))
            {
                string[] convertResult = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(entity.SerializedAdDetailsPictures);
                EntityFormMap(convertResult, entity);
                serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray);
                mainPictureSerializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray.First());
            }

            ViewBag.SerializedPictures = serializedString;
            ViewBag.MainPictureSerializedString = mainPictureSerializedString;
            #endregion


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
                if (ad == null)
                {
                    return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
                }
                if (ad.OwnerId != CurrentUserId)
                {
                    return RedirectToAction(nameof(Index)).WithError(LOCALIZATION_ERROR_ONLY_OWNER_CAN_EDIT);
                }

                string serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(ad.AdDetail.AdDetailPictures.Select(f => f.File).ToList());
                string mainPicture = Newtonsoft.Json.JsonConvert.SerializeObject(ad.MainPictureThumbnail);
                ViewBag.SerializedPictures = serializedString;
                ViewBag.MainPictureSerializedString = mainPicture;

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

                    #region File Map
                    //need to map files in controller because aspnet core assembly is present in adsite project
                    string[] result;
                    if (!String.IsNullOrEmpty(entity.SerializedAdDetailsPictures))
                    {
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(entity.SerializedAdDetailsPictures);
                        entity.MainPictureFile = StripComments(entity.MainPictureFile);
                        entity.MainPictureFile = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(entity.MainPictureFile);
                        EntityFormMap(result, entity);
                    }
                    else
                    {
                        MapFiles(entity);
                    }
                    //
                    #endregion

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

                    return RedirectToAction(nameof(MyAds)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                }
                else
                {
                    _logger.LogError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                    return NotFound().WithError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                }
            }


            ViewBag.Cities = _cityService.GetAllAsLookup(CountryId);
            ViewBag.Categories = _categoryService.GetAllAsLookup(CountryId);

            #region Preserve pictures if model validation fails 
            string serializedString = String.Empty;
            string mainPictureSerializedString = string.Empty;
            if (entity.Files != null && entity.Files.Count() > 0)
            {
                MapFiles(entity);
                serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray);
                mainPictureSerializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray.First());
            }
            if (!String.IsNullOrEmpty(entity.SerializedAdDetailsPictures))
            {
                string[] convertResult = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(entity.SerializedAdDetailsPictures);
                EntityFormMap(convertResult, entity);
                serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray);
                mainPictureSerializedString = Newtonsoft.Json.JsonConvert.SerializeObject(entity.FilesAsListOfByteArray.First());
            }

            ViewBag.SerializedPictures = serializedString;
            ViewBag.MainPictureSerializedString = mainPictureSerializedString;
            #endregion


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
                return RedirectToAction(nameof(Index)).WithError(ex.Message);
            }

            return RedirectToAction(nameof(MyAds)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
        }

        private bool AdExists(Guid id)
        {
            return _adService.Exists(id);
        }

        private int AdCount()
        {
            return _adService.Count();
        }

        private void MapFiles(AdEditModel entity)
        {
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
        }

        private void MapFiles(AdCreateModel entity)
        {
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
        }

        private void EntityFormMap(string[] array, AdCreateModel entity)
        {
            if (array != null)
            {
                foreach (var byteStream in array)
                {
                    byte[] byteArray = System.Convert.FromBase64String(byteStream);
                    using (MemoryStream stream = new MemoryStream(byteArray))
                        entity.FilesAsListOfByteArray.Add(stream.ToArray());
                }
            }
        }

        private void EntityFormMap(string[] array, AdEditModel entity)
        {
            if (array != null)
            {
                foreach (var byteStream in array)
                {
                    byte[] byteArray = System.Convert.FromBase64String(byteStream);
                    using (MemoryStream stream = new MemoryStream(byteArray))
                        entity.FilesAsListOfByteArray.Add(stream.ToArray());
                }
            }
        }

        private static string StripComments(string input)
        {
            // JavaScriptSerializer doesn't accept commented-out JSON, 
            // so we'll strip them out ourselves;
            // NOTE: for safety and simplicity, we only support comments on their own lines, 
            // not sharing lines with real JSON

            input = Regex.Replace(input, @"^\s*//.*$", "", RegexOptions.Multiline);  // removes comments like this
            input = Regex.Replace(input, @"^\s*/\*(\s|\S)*?\*/\s*$", "", RegexOptions.Multiline); /* comments like this */

            return input;
        }


    }
}
