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
        List<LocalizationViewModel> GetAll(Guid countryId);
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
                throw new Exception("Language couldnt be found");
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
                _logger.LogError("Exception while deleting categories : {0} - {1} ", ex.StackTrace, ex.Message);
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
                throw new Exception("Localization entity cannot be found");
            }

            return LocalizationMapper.MapToLocalizationEditModel(entity);
        }

        public LocalizationViewModel GetLocalizationAsViewModel(Guid localizationId)
        {
            var entity = _repository.Get(localizationId);
            if(entity == null)
            {
                throw new Exception("Localization entity cannot be found");
            }

            return LocalizationMapper.MapToLocalizationViewModel(entity);
        }

        public List<LocalizationViewModel> GetAll(Guid countryId)
        {
            var entities = _repository.GetAll(countryId);


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
                _logger.LogWarning("Cannot Find localization key with value : " + localizationKey);
                return localizationKey;
            }
        }

        public bool Update(LocalizationEditModel entity)
        {
            var localization = _repository.Get(entity.Id);
            if(localization == null)
            {
                throw new Exception("Entity with ID " + entity.Id + "cannot be found");
            }

            var language = _languageRepository.Get(entity.LanguageId);
            if (language == null)
            {
                throw new Exception("Language couldnt be found");
            }

            localization.LocalizationKey = entity.LocalizationKey;
            localization.LocalizationValue = entity.LocalizationValue;
            localization.Language = language;

            return _repository.Update(localization);
        }

    }
}
