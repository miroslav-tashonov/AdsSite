using AdSite.Extensions;
using AdSite.Models;
using AdSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace AdSite.Controllers
{
    [Authorize(Roles = ("User, Admin"))]
    public class WishlistController : Controller
    {
        string COUNTRY_ID = "CountryId";

        private readonly IWishlistService _wishlistService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<WishlistController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        private int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private const int SERVER_ERROR_CODE = 500;
        private Guid CountryId => _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);
        private string CurrentUserId => _userManager.GetUserId(User);

        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_WARNING_INVALID_MODELSTATE => _localizationService.GetByKey("WarningMessage_ModelStateInvalid", CultureId);
        private string LOCALIZATION_ERROR_ALREADYEXISTS => _localizationService.GetByKey("ErrorMessage_AlreadyExist", CultureId);
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

        public IActionResult Create(Guid adId)
        {
            if (adId == null)
            {
                throw new Exception(LOCALIZATION_ERROR_NOT_FOUND);
            }

            string referer = Request.Headers["Referer"].ToString();
            if (!String.IsNullOrEmpty(CurrentUserId))
            {
                try
                {
                    bool statusResult = _wishlistService.Add(adId, CurrentUserId, CountryId);

                    if (statusResult)
                    {
                        return Redirect(referer).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                    }
                    else
                    {
                        return Redirect(referer).WithError(LOCALIZATION_ERROR_ALREADYEXISTS);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return Redirect(referer).WithError(ex.Message);
                }
            }
            else
            {
                _logger.LogError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
                return Redirect(referer).WithError(LOCALIZATION_ERROR_USER_MUST_LOGIN);
            }
        }


        public IActionResult Delete(Guid adId)
        {
            string referer = Request.Headers["Referer"].ToString();
            if (adId == null)
            {
                return Redirect(referer).WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            try
            {
                _wishlistService.Delete(adId, CurrentUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Redirect(referer).WithError(ex.Message);
            }

            return Redirect(referer).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
        }

        [AllowAnonymous]
        private bool WishlistExists(Guid id)
        {
            return _wishlistService.Exists(id);
        }
    }
}
