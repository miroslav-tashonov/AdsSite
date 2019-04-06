using AdSite.Data.Repositories;
using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using AdSite.Models.Mappers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdSite.Services
{
    public interface ILocalizationService
    {
        LocalizationEditModel GetLocalizationAsEditModel(Guid localizationId);
        LocalizationViewModel GetLocalizationAsViewModel(Guid localizationId);
        string GetByKey(string localizationKey, int cultureId);
        List<LocalizationViewModel> GetAll(string columnName, string searchString, Guid countryId);
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(LocalizationCreateModel localization);
        bool Update(LocalizationEditModel localization);
    }

    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationRepository _repository;
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger<LocalizationService> _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_LOCALIZATIONCLASS_NOT_FOUND => _repository.GetLocalizationValue("Localization_LocalizationClass_Not_Found", CultureId);
        private string LOCALIZATION_LOCALIZATIONCLASS_KEY_NOT_FOUND => _repository.GetLocalizationValue("Localization_LocalizationClass_Key_Not_Found", CultureId);
        private string LOCALIZATION_LOCALIZATIONCLASS_ENTITY_NOT_FOUND => _repository.GetLocalizationValue("Localization_LocalizationClass__Entity_Not_Found", CultureId);



        public LocalizationService(ILocalizationRepository repository, ILanguageRepository languageRepository, ILogger<LocalizationService> logger)
        {
            _repository = repository;
            _languageRepository = languageRepository;
            _logger = logger;
        }

        public bool Add(LocalizationCreateModel entity)
        {
            var language = _languageRepository.Get(entity.LanguageId);
            if (language == null)
            {
                throw new Exception(LOCALIZATION_LOCALIZATIONCLASS_NOT_FOUND);
            }

            Localization localization = new Localization
            {
                LocalizationKey = entity.LocalizationKey,
                LocalizationValue = entity.LocalizationValue,
                Language = language,
                CountryId = entity.CountryId
            };

            return _repository.Add(localization);
        }

        public bool Delete(Guid id)
        {
            try
            {
                return _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while deleting localization : {0} - {1} ", ex.StackTrace, ex.Message);
                throw ex;
            }
        }

        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public LocalizationEditModel GetLocalizationAsEditModel(Guid localizationId)
        {
            var entity = _repository.Get(localizationId);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_LOCALIZATIONCLASS_NOT_FOUND);
            }

            return LocalizationMapper.MapToLocalizationEditModel(entity);
        }

        public LocalizationViewModel GetLocalizationAsViewModel(Guid localizationId)
        {
            var entity = _repository.Get(localizationId);
            if(entity == null)
            {
                throw new Exception(LOCALIZATION_LOCALIZATIONCLASS_NOT_FOUND);
            }

            return LocalizationMapper.MapToLocalizationViewModel(entity);
        }
        
        public List<LocalizationViewModel> GetAll(string columnName, string searchString, Guid countryId)
        {
            List<Localization> entities;

            switch (columnName.ToLower())
            {
                case "localizationkey":
                    entities = _repository.GetByLocalizationKey(searchString, countryId);
                    break;
                case "localizationvalue":
                    entities = _repository.GetByLocalizationValue(searchString, countryId);
                    break;
                default:
                    entities = _repository.GetAll(countryId);
                    break;
            }

            if (entities == null)
            {
                throw new Exception(LOCALIZATION_LOCALIZATIONCLASS_NOT_FOUND);
            }

            return LocalizationMapper.MapToLocalizationViewModel(entities);
        }

        public string GetByKey(string localizationKey, int cultureId)
        {
            try
            {
                return _repository.GetLocalizationValue(localizationKey, cultureId);
            }
            catch
            {
                _logger.LogWarning(LOCALIZATION_LOCALIZATIONCLASS_KEY_NOT_FOUND + localizationKey);
                return localizationKey;
            }
        }

        public bool Update(LocalizationEditModel entity)
        {
            var localization = _repository.Get(entity.Id);
            if(localization == null)
            {
                throw new Exception(LOCALIZATION_LOCALIZATIONCLASS_ENTITY_NOT_FOUND  + entity.Id);
            }

            var language = _languageRepository.Get(entity.LanguageId);
            if (language == null)
            {
                throw new Exception(LOCALIZATION_LOCALIZATIONCLASS_NOT_FOUND);
            }

            localization.LocalizationKey = entity.LocalizationKey;
            localization.LocalizationValue = entity.LocalizationValue;
            localization.Language = language;

            return _repository.Update(localization);
        }

    }
}
