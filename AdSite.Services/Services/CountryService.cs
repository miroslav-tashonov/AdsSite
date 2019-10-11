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
using System.Threading;
using Microsoft.AspNetCore.Builder;

namespace AdSite.Services
{
    public interface ICountryService
    {
        Guid Get(Guid countryId);
        CountryViewModel GetByCountryPath(string path);
        List<CountryViewModel> GetAll();
        CountryViewModel GetCountryAsViewModel(Guid id);
        List<CountryViewModel> GetCountries(string columnName, string searchString);
        bool Add(CountryCreateModel entity);
        CountryEditModel GetCountryAsEditModel(Guid id);
        bool Exist(Guid Id);
        bool Update(CountryEditModel entity);
        bool Delete(Guid id);
    }



    public class CountryService : ICountryService
    {
        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_COUNTRY_NOT_FOUND =>
            _localizationRepository.GetLocalizationValue("Localization_City_Not_Found", CultureId);
        private string LOCALIZATION_GENERAL_NOT_FOUND =>
            _localizationRepository.GetLocalizationValue("Localization_General_Not_Found", CultureId);


        private readonly ICountryRepository _repository;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ILogger _logger;
        public CountryService(ICountryRepository repository, ILocalizationRepository localizationRepository,
            ILogger<CountryService> logger)
        {
            _repository = repository;
            _localizationRepository = localizationRepository;
            _logger = logger;
        }

        public bool Add(CountryCreateModel entity)
        {
            if(GetByCountryPath(entity.Path).Path.Length > 0)
            {
                throw new Exception("Country path already exist");
            }

            Country country = new Country
            {
                Name = entity.Name,
                Path = entity.Path,
                Abbreviation = entity.Abbreviation,
                CreatedBy = entity.CreatedBy,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = entity.ModifiedAt,
                ModifiedBy = entity.ModifiedBy
            };

            return _repository.Add(country);
        }

        public Guid Get(Guid countryId)
        {
            return _repository.Get(countryId).ID;
        }

        public List<CountryViewModel> GetAll()
        {
            return CountryMapper.MapToCountryViewModel(_repository.GetAll());
        }

        public CountryViewModel GetByCountryPath(string path)
        {
            return CountryMapper.MapToCountryViewModel(_repository.GetCountryByPath(path));
        }

        public CountryViewModel GetCountryAsViewModel(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_COUNTRY_NOT_FOUND);
            }

            return CountryMapper.MapToCountryViewModel(entity);
        }

        public CountryEditModel GetCountryAsEditModel(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_COUNTRY_NOT_FOUND);
            }

            return CountryMapper.MapToCountryEditModel(entity);
        }

        public List<CountryViewModel> GetCountries(string columnName, string searchString)
        {
            List<Country> entities;

            switch (columnName.ToLower())
            {
                case "name":
                    entities = _repository.GetByCountryName(searchString);
                    break;
                case "path":
                    entities = _repository.GetByCountryPath(searchString);
                    break;
                default:
                    entities = _repository.GetAll();
                    break;
            }

            if (entities == null)
            {
                throw new Exception(LOCALIZATION_COUNTRY_NOT_FOUND);
            }

            return CountryMapper.MapToCountryViewModel(entities);
        }

        public bool Exist(Guid ID)
        {
            return _repository.Exists(ID);
        }

        public bool Update(CountryEditModel entity)
        {
            if (GetByCountryPath(entity.Path).Path.Length > 0)
            {
                throw new Exception("Country path already exist");
            }

            Country country = _repository.Get(entity.ID);
            if (country == null)
            {
                throw new Exception(LOCALIZATION_GENERAL_NOT_FOUND + entity.ID);
            }

            country.Name = entity.Name;
            country.Path = entity.Path;
            country.Abbreviation = entity.Abbreviation;

            country.ModifiedAt = entity.ModifiedAt;
            country.ModifiedBy = entity.ModifiedBy;

            return _repository.Update(country);
        }


        public bool Delete(Guid id)
        {
            try
            {
                return _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while deleting country : {0} - {1} ", ex.StackTrace, ex.Message);
                throw ex;
            }
        }

    }
}
