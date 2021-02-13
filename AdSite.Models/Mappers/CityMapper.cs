using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class CityMapper
    {

        public static CityEditModel MapToCityEditModel(City entity)
        {
            var editModel = new CityEditModel();

            editModel.ID = entity.ID;
            editModel.Name = entity.Name;
            editModel.Postcode = entity.Postcode;

            return editModel;
        }

        public static CityViewModel MapToCityViewModel(City entity)
        {
            return new CityViewModel()
            {
                ID = entity.ID,
                Name = entity.Name,
                Postcode = entity.Postcode,
            };
        }

        public static List<LookupViewModel> MapToLookupViewModel(List<City> cities)
        {
            var listViewModel = new List<LookupViewModel>();
            if (cities != null && cities.Count > 0)
            {
                foreach (City city in cities)
                {
                    var viewModel = new LookupViewModel();

                    viewModel.Id = city.ID;
                    viewModel.Name = city.Name;

                    listViewModel.Add(viewModel);
                }
            }
            return listViewModel;
        }

        public static List<CityViewModel> MapToCityViewModel(List<City> entities)
        {
            List<CityViewModel> viewModelList = new List<CityViewModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    viewModelList.Add(
                        new CityViewModel()
                        {
                            ID = entity.ID,
                            Name = entity.Name,
                            Postcode = entity.Postcode,
                            CreatedAt = entity.CreatedAt,
                            ModifiedAt = entity.ModifiedAt
                        }
                    );
                }
            }

            return viewModelList;
        }

    }
}
