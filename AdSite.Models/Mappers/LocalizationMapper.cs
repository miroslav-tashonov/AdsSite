using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class LocalizationMapper
    {

        public static LocalizationEditModel MapToLocalizationEditModel(Localization localization)
        {
            var viewModel = new LocalizationEditModel();

            viewModel.Id = localization.ID;
            viewModel.LocalizationKey = localization.LocalizationKey;
            viewModel.LocalizationValue = localization.LocalizationValue;
            viewModel.LanguageId = localization.Language.ID;

            return viewModel;
        }

        public static LocalizationViewModel MapToLookupViewModel(Localization localization)
        {
            return new LocalizationViewModel()
            {
                Id = localization.ID,
                LocalizationKey = localization.LocalizationKey,
                LocalizationValue = localization.LocalizationValue,
                Language = new LookupViewModel() { Id = localization.Language.ID, Name = localization.Language.LanguageName }
            };
        }

        public static List<LocalizationViewModel> MapToLookupViewModel(List<Localization> localizations)
        {
            List<LocalizationViewModel> viewModelList = new List<LocalizationViewModel>();

            if (localizations != null && localizations.Count > 0)
            {
                foreach (var localization in localizations)
                {
                    viewModelList.Add(
                        new LocalizationViewModel()
                        {
                            Id = localization.ID,
                            LocalizationKey = localization.LocalizationKey,
                            LocalizationValue = localization.LocalizationValue,
                            Language = new LookupViewModel() { Id = localization.Language.ID, Name = localization.Language.LanguageName },
                        }
                    );
                }
            }

            return viewModelList;
        }

    }
}
