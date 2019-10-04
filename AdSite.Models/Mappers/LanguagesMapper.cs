using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class LanguagesMapper
    {
        public static List<LanguageViewModel> MapToViewModel(List<Language> languages)
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

        public static LanguageViewModel MapToViewModel(Language language)
        {
            var viewModel = new LanguageViewModel();

            viewModel.ID = language.ID;
            viewModel.CultureId = language.CultureId;
            viewModel.LanguageName = language.LanguageName;
            viewModel.LanguageShortName = language.LanguageShortName;

            return viewModel;
        }

        public static List<LookupViewModel> MapToLookupViewModel(List<Language> languages)
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
