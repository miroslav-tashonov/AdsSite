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

namespace AdSite.Services
{
    public interface ILanguageService
    {
        bool Exists(int lcid, Guid countryId);
        bool Exists(Guid id);

        bool Delete(Guid id, Guid countryId);
        bool Add(LanguageCreateModel language);
        List<LanguageViewModel> GetAll(Guid country);

        List<LookupViewModel> GetAllAsLookup(Guid country);

    }



    public class LanguageService : ILanguageService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILanguageRepository _repository;
        private readonly ILogger _logger;

        private readonly int NUMBER_OF_LANGUAGES_REQUIRED_PER_COUNTRY = 1;

        public LanguageService(ILanguageRepository repository, ILogger<LanguageService> logger, UserManager<ApplicationUser> userManager)
        {
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
                    throw new Exception("Language is already added.");
                }
            }
            else
            {
                throw new Exception("Cannot find Culture with cultureId ");
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
                    _logger.LogError("Cannot delete the last language.");
                    throw new Exception("Cannot delete the last language.");
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

        public List<LanguageViewModel> GetAll(Guid countryId)
        {
            return MapToViewModel(_repository.GetAll(countryId));
        }

        public List<LookupViewModel> GetAllAsLookup(Guid countryId)
        {
            return MapToLookupViewModel(_repository.GetAll(countryId));
        }

        private List<LanguageViewModel> MapToViewModel(List<Language> languages)
        {
            var listViewModel = new List<LanguageViewModel>();
            if (languages != null && languages.Count > 0)
            {
                foreach (Language language in languages)
                {
                    var viewModel = new LanguageViewModel();

                    viewModel.ID = language.ID;
                    viewModel.CultureId = language.CultureId;
                    viewModel.LanguageName = language.LanguageName;
                    viewModel.LanguageShortName = language.LanguageShortName;

                    listViewModel.Add(viewModel);
                }
            }
            return listViewModel;
        }

        private List<LookupViewModel> MapToLookupViewModel(List<Language> languages)
        {
            var listViewModel = new List<LookupViewModel>();
            if (languages != null && languages.Count > 0)
            {
                foreach (Language language in languages)
                {
                    var viewModel = new LookupViewModel();

                    viewModel.Id = language.ID;
                    viewModel.Name = language.LanguageName;

                    listViewModel.Add(viewModel);
                }
            }
            return listViewModel;
        }
    }
}
