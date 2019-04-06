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
            var editModel = new LocalizationEditModel();

            editModel.Id = localization.ID;
            editModel.LocalizationKey = localization.LocalizationKey;
            editModel.LocalizationValue = localization.LocalizationValue;
            editModel.LanguageId = localization.Language.ID;

            return editModel;
        }


        public static LocalizationViewModel MapToLocalizationViewModel(Localization localization)
        {
            return new LocalizationViewModel()
            {
                Id = localization.ID,
                LocalizationKey = localization.LocalizationKey,
                LocalizationValue = localization.LocalizationValue,
                Language = new LookupViewModel() { Id = localization.Language.ID, Name = localization.Language.LanguageName }
            };
        }

        public static List<LocalizationViewModel> MapToLocalizationViewModel(List<Localization> localizations)
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
