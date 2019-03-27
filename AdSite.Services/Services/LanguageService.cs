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
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(LanguageCreateModel language);
        bool Update(LanguageEditModel language);
        List<LanguageViewModel> GetAll(Guid country);
    }



    public class LanguageService : ILanguageService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILanguageRepository _repository;
        private readonly ILogger _logger;
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
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return false;
                }
            }
            else
            {
                _logger.LogError("Cannot find Culture with cultureId ");
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                _repository.Delete(id);
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

        public bool Update(LanguageEditModel entity)
        {
            Language language = _repository.Get(entity.ID);
            if (language == null)
            {
                throw new Exception("Cannot find language with id " + entity.ID);
            }

            language.CultureId = entity.CultureId;
            language.LanguageName = entity.LanguageName;
            language.LanguageShortName = entity.LanguageShortName;

            return _repository.Update(language);
        }


        public List<LanguageViewModel> GetAll(Guid countryId)
        {
            return MapToViewModel(_repository.GetAll(countryId));
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
    }
}
