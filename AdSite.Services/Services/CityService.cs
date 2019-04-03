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
    public interface ICityService
    {
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(CityCreateModel category);
        bool Update(CityEditModel category);
        List<CityViewModel> GetCities(Guid countryId);
        List<CityViewModel> GetCities(string columnName, string searchString, Guid countryId);
        CityViewModel GetCityAsViewModel(Guid cityId);
        CityEditModel GetCityAsEditModel(Guid cityId);
    }



    public class CityService : ICityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICityRepository _repository;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_CITY_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_City_Not_Found", CultureId);
        private string LOCALIZATION_GENERAL_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_General_Not_Found", CultureId);

        public CityService(ICityRepository repository, ILocalizationRepository localizationRepository, ILogger<CityService> logger, UserManager<ApplicationUser> userManager)
        {
            _localizationRepository = localizationRepository;
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public bool Add(CityCreateModel entity)
        {
            City city = new City
            {
                Name = entity.Name,
                Postcode = entity.Postcode,
                CreatedBy = entity.CreatedBy,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = entity.ModifiedAt,
                ModifiedBy = entity.ModifiedBy,
                CountryId = entity.CountryId
            };


            return _repository.Add(city);
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

        public List<CityViewModel> GetCities(Guid countryId)
        {
            var entities = _repository.GetAll(countryId);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_CITY_NOT_FOUND);
            }

            return CityMapper.MapToCityViewModel(entities);
        }

        public List<CityViewModel> GetCities(string columnName, string searchString, Guid countryId)
        {
            List<City> entities;

            switch (columnName.ToLower())
            {
                case "name":
                    entities = _repository.GetByCityName(searchString, countryId);
                    break;
                case "postcode":
                    entities = _repository.GetByCityPostcode(searchString, countryId);
                    break;
                default:
                    entities = _repository.GetAll(countryId);
                    break;
            }

            if (entities == null)
            {
                throw new Exception(LOCALIZATION_CITY_NOT_FOUND);
            }

            return CityMapper.MapToCityViewModel(entities);
        }

        public CityEditModel GetCityAsEditModel(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_CITY_NOT_FOUND);
            }

            return CityMapper.MapToCityEditModel(entity);
        }

        public CityViewModel GetCityAsViewModel(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_CITY_NOT_FOUND);
            }

            return CityMapper.MapToCityViewModel(entity);
        }
        public bool Update(CityEditModel entity)
        {
            City city = _repository.Get(entity.ID);
            if (city == null)
            {
                throw new Exception(LOCALIZATION_GENERAL_NOT_FOUND + entity.ID);
            }

            city.Name = entity.Name;
            city.Postcode = entity.Postcode;

            city.ModifiedAt = entity.ModifiedAt;
            city.ModifiedBy = entity.ModifiedBy;

            return _repository.Update(city);
        }


    }
}
