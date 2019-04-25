using AdSite.Extensions;
using AdSite.Mappers;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace AdSite.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<WishlistController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        private int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private const int SERVER_ERROR_CODE = 500;
        private Guid CountryId => _countryService.Get();
        private string CurrentUserId => _userManager.GetUserId(User);

        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_WARNING_INVALID_MODELSTATE => _localizationService.GetByKey("WarningMessage_ModelStateInvalid", CultureId);
        private string LOCALIZATION_ERROR_DEFAULT => _localizationService.GetByKey("ErrorMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_USER_MUST_LOGIN => _localizationService.GetByKey("ErrorMessage_MustLogin", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);
        private string LOCALIZATION_ERROR_CONCURENT_EDIT => _localizationService.GetByKey("ErrorMessage_ConcurrentEdit", CultureId);

        public WishlistController(IWishlistService wishlistService, ICountryService countryService, ILocalizationService localizationService, ILogger<WishlistController> logger, UserManager<ApplicationUser> userManager)
        {
            _wishlistService = wishlistService;
            _countryService = countryService;
            _localizationService = localizationService;
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            try
            {
                return View(_wishlistService.GetMyWishlist(CurrentUserId, CountryId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(ex.Message).WithError(ex.Message);
            }
        }

        // GET: Cities/Create
        public IActionResult Create(Guid adId)
        {
            if(adId == null)
            {
                throw new Exception(LOCALIZATION_ERROR_NOT_FOUND);
            }


            if (!String.IsNullOrEmpty(CurrentUserId))
            {
                try
                {
                    bool statusResult = _wishlistService.Add(adId, CurrentUserId);
                    if (statusResult)
                    {
                        return Ok().WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                    }
                    else
                    {
                        return StatusCode(SERVER_ERROR_CODE).WithError(LOCALIZATION_ERROR_DEFAULT);
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


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            try
            {
                _wishlistService.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(SERVER_ERROR_CODE).WithError(ex.Message);
            }

            return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
        }

        private bool CityExists(Guid id)
        {
            return _wishlistService.Exists(id);
        }
    }
}
