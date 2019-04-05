using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class AdMapper
    {

        public static AdEditModel MapToAdEditModel(Ad entity)
        {
            var editModel = new AdEditModel();

            editModel.ID = entity.ID;
            editModel.Name = entity.Name;

            return editModel;
        }

        public static AdViewModel MapToAdViewModel(Ad entity)
        {
            return new AdViewModel()
            {
                Name = entity.Name,
            };
        }

        public static List<AdViewModel> MapToAdViewModel(List<Ad> entities)
        {
            List<AdViewModel> viewModelList = new List<AdViewModel>();

            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    viewModelList.Add(
                        new AdViewModel()
                        {
                            Name = entity.Name,
                        }
                    );
                }
            }

            return viewModelList;
        }

    }
}
