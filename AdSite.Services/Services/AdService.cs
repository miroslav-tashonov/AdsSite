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
    public interface IAdService
    {
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(AdCreateModel ad);
        bool Update(AdEditModel ad);
        List<AdViewModel> GetAds(Guid countryId);
        List<AdViewModel> GetAds(string columnName, string searchString, Guid countryId);
        List<AdGridViewModel> GetAdGridModel(Guid countryId);
        AdViewModel GetAdAsViewModel(Guid adId);
        AdEditModel GetAdAsEditModel(Guid adId);
    }



    public class AdService : IAdService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdRepository _repository;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_AD_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_Ad_Not_Found", CultureId);
        private string LOCALIZATION_GENERAL_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_General_Not_Found", CultureId);

        public AdService(IAdRepository repository, ILocalizationRepository localizationRepository, ILogger<AdService> logger, UserManager<ApplicationUser> userManager)
        {
            _localizationRepository = localizationRepository;
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public bool Add(AdCreateModel entity)
        {
            List<AdDetailPicture> pictures = new List<AdDetailPicture>();
            if(entity.FilesAsListOfByteArray != null && entity.FilesAsListOfByteArray.Count > 0)
                foreach (var file in entity.FilesAsListOfByteArray)
                {
                    pictures.Add(new AdDetailPicture
                    {
                        File = file,
                        CreatedBy = entity.CreatedBy,
                        CreatedAt = entity.CreatedAt,
                        ModifiedAt = entity.ModifiedAt,
                        ModifiedBy = entity.ModifiedBy,
                    });
                }

            Ad ad = new Ad
            {
                Name = entity.Name,
                CategoryID = entity.CategoryId,
                CityID = entity.CityId,
                OwnerId = entity.OwnerId,
                CreatedBy = entity.CreatedBy,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = entity.ModifiedAt,
                ModifiedBy = entity.ModifiedBy,
                CountryID = entity.CountryId,
                AdDetail = new AdDetail
                {
                    Description = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    ModifiedBy = entity.ModifiedBy,
                    AdDetailPictures = pictures
                }
            };


            return _repository.Add(ad);
        }

        public bool Delete(Guid id)
        {
            try
            {
                return _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while deleting cities : {0} - {1} ", ex.StackTrace, ex.Message);
                throw ex;
            }
        }


        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public List<AdViewModel> GetAds(Guid countryId)
        {
            var entities = _repository.GetAll(countryId);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdViewModel(entities);
        }

        public List<AdViewModel> GetAds(string columnName, string searchString, Guid countryId)
        {
            List<Ad> entities;

            switch (columnName.ToLower())
            {
                case "name":
                    entities = _repository.GetByAdName(searchString, countryId);
                    break;
                default:
                    entities = _repository.GetAll(countryId);
                    break;
            }

            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdViewModel(entities);
        }

        public AdEditModel GetAdAsEditModel(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdEditModel(entity);
        }

        public AdViewModel GetAdAsViewModel(Guid id)
        {
            var entity = _repository.GetAdWithDetails(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdViewModel(entity);
        }

        public List<AdGridViewModel> GetAdGridModel(Guid countryId)
        {
            var entities = _repository.GetAdGrid(countryId);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdGridModel(entities);
        }

        public bool Update(AdEditModel entity)
        {
            Ad ad = _repository.Get(entity.ID);
            if (ad == null)
            {
                throw new Exception(LOCALIZATION_GENERAL_NOT_FOUND + entity.ID);
            }

            ad.OwnerId = entity.OwnerId;
            ad.CityID = entity.CityId;
            ad.CategoryID = entity.CategoryId;

            ad.ModifiedAt = entity.ModifiedAt;
            ad.ModifiedBy = entity.ModifiedBy;

            ad.Price = entity.Price;
            ad.Name = entity.Name;
            ad.ModifiedAt = entity.ModifiedAt;
            ad.ModifiedBy = entity.ModifiedBy;

            return _repository.Update(ad);
        }


    }
}
