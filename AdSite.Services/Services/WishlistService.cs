﻿using AdSite.Data.Repositories;
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
        bool Delete(Guid id);
        bool Add(Guid adId, string currentUserId);
        List<WishlistViewModel> GetMyWishlist(string ownerId, Guid countryId);
    }



    public class WishlistService : IWishlistService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWishlistRepository _repository;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_GENERAL_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_General_Not_Found", CultureId);

        public WishlistService(IWishlistRepository repository, ILocalizationRepository localizationRepository, ILogger<WishlistService> logger, UserManager<ApplicationUser> userManager)
        {
            _localizationRepository = localizationRepository;
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public bool Add(Guid adId, string currentUserId)
        {
            Wishlist wishlist = new Wishlist
            {
                AdId = adId,
                OwnerId = currentUserId
            };


            return _repository.Add(wishlist);
        }

        public bool Delete(Guid id)
        {
            try
            {
                return _repository.Delete(id);
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


        public List<WishlistViewModel> GetMyWishlist(string ownerId, Guid countryId)
        {
            return WishlistMapper.MapToWishlistViewModel(_repository.GetAll(ownerId, countryId));
        }

    }
}