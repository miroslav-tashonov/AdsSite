using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class CountryMapper
    {

        public static CountryEditModel MapToCountryEditModel(Country entity)
        {
            var editModel = new CountryEditModel();

            editModel.ID = entity.ID;
            editModel.Name = entity.Name;
            editModel.Path = entity.Path;
            editModel.Abbreviation = entity.Abbreviation;

            return editModel;
        }

        public static CountryViewModel MapToCountryViewModel(Country entity)
        {
            return new CountryViewModel()
            {
                ID = entity.ID,
                Name = entity.Name,
                Path = entity.Path,
                Abbreviation = entity.Abbreviation
            };
        }

        public static List<LookupViewModel> MapToLookupViewModel(List<Country> countries)
        {
            var listViewModel = new List<LookupViewModel>();
            if (countries != null && countries.Count > 0)
            {
                foreach (Country country in countries)
                {
                    var viewModel = new LookupViewModel();

                    viewModel.Id = country.ID;
                    viewModel.Name = country.Name;

                    listViewModel.Add(viewModel);
                }
            }
            return listViewModel;
        }

        public static List<CountryViewModel> MapToCountryViewModel(List<Country> entities)
        {
            List<CountryViewModel> viewModelList = new List<CountryViewModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    viewModelList.Add(
                        new CountryViewModel()
                        {
                            ID = entity.ID,
                            Name = entity.Name,
                            Path = entity.Path,
                            Abbreviation = entity.Abbreviation
                        }
                    );
                }
            }

            return viewModelList;
        }

    }
}
