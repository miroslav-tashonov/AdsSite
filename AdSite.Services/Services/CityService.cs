using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using AdSite.Models.Mappers;

namespace AdSite.Services
{
    public interface ICityService
    {
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(CityCreateModel category);
        bool Update(CityEditModel category);
        List<CityViewModel> GetCities(Guid countryId);
        CityViewModel GetCityAsViewModel(Guid cityId);
        CityEditModel GetCityAsEditModel(Guid cityId);
    }



    public class CityService : ICityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICityRepository _repository;
        private readonly ILogger _logger;
        public CityService(ICityRepository repository, ILogger<CityService> logger, UserManager<ApplicationUser> userManager)
        {
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
            catch(Exception ex)
            {
                _logger.LogError("Exception while deleting categories : {0} - {1} ", ex.StackTrace, ex.Message);
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
                throw new Exception("City entity cannot be found");
            }

            return CityMapper.MapToCityViewModel(entities);
        }
        public CityEditModel GetCityAsEditModel(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
            {
                throw new Exception("City entity cannot be found");
            }

            return CityMapper.MapToCityEditModel(entity);
        }

        public CityViewModel GetCityAsViewModel(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
            {
                throw new Exception("City entity cannot be found");
            }

            return CityMapper.MapToCityViewModel(entity);
        }
        public bool Update(CityEditModel entity)
        {
            City city = _repository.Get(entity.ID);
            if(city == null)
            {
                throw new Exception("Entity with ID " + entity.ID + " couldnt be found.");
            }

            city.Name = entity.Name;
            city.Postcode = entity.Postcode;

            city.ModifiedAt = entity.ModifiedAt;
            city.ModifiedBy = entity.ModifiedBy;

            return _repository.Update(city);
        }


    }
}
