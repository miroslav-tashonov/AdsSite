using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using AdSite.Models.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AdSite.Services
{
    public interface IWishlistService
    {
        bool Exists(Guid id);
        bool Delete(Guid adId, string currentUserId);
        bool Add(Guid adId, string currentUserId, Guid countryId);
        bool IsInWishlist(Guid adId, string userId);
        List<WishlistGridModel> GetMyWishlist(string ownerId, Guid countryId);
    }



    public class WishlistService : IWishlistService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWishlistRepository _repository;
        private readonly IAdService _adService;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_GENERAL_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_General_Not_Found", CultureId);

        public WishlistService(IWishlistRepository repository, ILocalizationRepository localizationRepository, IAdService adService, ILogger<WishlistService> logger, UserManager<ApplicationUser> userManager)
        {
            _localizationRepository = localizationRepository;
            _adService = adService;
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public bool Add(Guid adId, string currentUserId, Guid countryId)
        {
            Wishlist wishlist = new Wishlist
            {
                AdId = adId,
                OwnerId = currentUserId,
                CountryId = countryId
            };

            if (!_repository.Exists(adId, currentUserId))
            {
                return _repository.Add(wishlist);
            }
            else
                return false;
        }

        public bool Delete(Guid adId, string currentUserID)
        {
            try
            {
                return _repository.Delete(adId, currentUserID);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while deleting wishlist : {0} - {1} ", ex.StackTrace, ex.Message);
                throw ex;
            }
        }


        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public bool IsInWishlist(Guid adId, string userId)
        {
            return _repository.Exists(adId, userId);
        }


        public List<WishlistGridModel> GetMyWishlist(string ownerId, Guid countryId)
        {
            List<WishlistGridModel> listGridModel = new List<WishlistGridModel>();
            var wishlists = _repository.GetAll(ownerId, countryId);

            foreach (var wishlist in wishlists)
            {
                listGridModel.Add( WishlistMapper.MapToWishlistGridModel( wishlist, _adService.GetAdAsAdWishlistGridModel(wishlist.AdId) ));
            }

            return listGridModel;
        }

    }
}
