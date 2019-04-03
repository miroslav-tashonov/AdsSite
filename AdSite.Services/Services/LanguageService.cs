using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Threading;
using AdSite.Models.Mappers;

namespace AdSite.Services
{
    public interface ILanguageService
    {
        bool Exists(int lcid, Guid countryId);
        bool Exists(Guid id);

        bool Delete(Guid id, Guid countryId);
        bool Add(LanguageCreateModel language);

        List<LanguageViewModel> GetAll(Guid country);
        List<LanguageViewModel> GetAll(string columnName, string searchString, Guid country);

        List<LookupViewModel> GetAllAsLookup(Guid country);

    }



    public class LanguageService : ILanguageService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILanguageRepository _repository;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_LANGUAGE_CULTURE_MISSING => _localizationRepository.GetLocalizationValue("Localization_Language_Culture_Missing", CultureId);
        private string LOCALIZATION_LANGUAGE_LAST => _localizationRepository.GetLocalizationValue("Localization_Language_Last", CultureId);
        private string LOCALIZATION_LANGUAGE_ALREADY_ADDED=> _localizationRepository.GetLocalizationValue("Localization_Language_Already_Added", CultureId);
        private string LOCALIZATION_LANGUAGE_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_Language_Not_Found", CultureId);

        private readonly int NUMBER_OF_LANGUAGES_REQUIRED_PER_COUNTRY = 1;

        public LanguageService(ILanguageRepository repository, ILocalizationRepository localizationRepository, ILogger<LanguageService> logger, UserManager<ApplicationUser> userManager)
        {
            _localizationRepository = localizationRepository;
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public bool Add(LanguageCreateModel entity)
        {
            
            int cultureId;
            int.TryParse(entity.CultureId, out cultureId);
            if (cultureId > 0)
            {
                if (!Exists(cultureId, entity.CountryId))
                {
                    try
                    {
                        CultureInfo cultureInfo = new CultureInfo(cultureId);
                        Language language = new Language
                        {
                            CultureId = cultureId,
                            LanguageName = cultureInfo.DisplayName,
                            LanguageShortName = cultureInfo.Name,
                            CountryId = entity.CountryId
                        };

                        return _repository.Add(language);
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw new Exception(LOCALIZATION_LANGUAGE_ALREADY_ADDED);
                }
            }
            else
            {
                throw new Exception(LOCALIZATION_LANGUAGE_CULTURE_MISSING);
            }
        }

        public bool Delete(Guid id, Guid countryId)
        {
            try
            {
                if (_repository.Count(countryId) > NUMBER_OF_LANGUAGES_REQUIRED_PER_COUNTRY)
                {
                    _repository.Delete(id);
                }
                else
                {
                    _logger.LogError(LOCALIZATION_LANGUAGE_LAST);
                    throw new Exception(LOCALIZATION_LANGUAGE_LAST);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while deleting language : {0} - {1} ", ex.StackTrace, ex.Message);
                return false;
            }

            return true;
        }

        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public bool Exists(int lcid, Guid countryId)
        {
            return _repository.Exists(lcid, countryId);
        }

        public List<LanguageViewModel> GetAll(string columnName, string searchString, Guid countryId)
        {
            List<Language> entities;

            switch (columnName.ToLower())
            {
                case "languagename":
                    entities = _repository.GetByLanguageName(searchString, countryId);
                    break;
                case "languageshortname":
                    entities = _repository.GetByLanguageShortName(searchString, countryId);
                    break;
                default:
                    entities = _repository.GetAll(countryId);
                    break;
            }

            if (entities == null)
            {
                throw new Exception(LOCALIZATION_LANGUAGE_NOT_FOUND);
            }

            return LanguagesMapper.MapToViewModel(entities);
        }

        public List<LanguageViewModel> GetAll(Guid countryId)
        {
            return LanguagesMapper.MapToViewModel(_repository.GetAll(countryId));
        }


        public List<LookupViewModel> GetAllAsLookup(Guid countryId)
        {
            return LanguagesMapper.MapToLookupViewModel(_repository.GetAll(countryId));
        }
    }
}
