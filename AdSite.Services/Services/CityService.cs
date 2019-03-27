using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdSite.Services
{
    public interface ICityService
    {
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(CityCreateModel category);
        bool Update(CityEditModel category);
        List<City> GetCities(Guid countryId);
        City GetCity(Guid cityId);
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

        public List<City> GetCities(Guid countryId)
        {
            return _repository.GetAll(countryId);
        }

        public City GetCity(Guid cityId)
        {
            return _repository.Get(cityId);
        }
        
        public bool Update(CityEditModel entity)
        {
            City city = _repository.Get(entity.ID);
            city.Name = entity.Name;

            city.ModifiedAt = entity.ModifiedAt;
            city.ModifiedBy = entity.ModifiedBy;

            return _repository.Update(city);
        }


    }
}
