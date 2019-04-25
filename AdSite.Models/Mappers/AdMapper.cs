using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
            editModel.OwnerId = entity.OwnerId;
            editModel.Price = entity.Price;
            editModel.CategoryId = entity.CategoryID;
            editModel.CityId = entity.CityID;


            return editModel;
        }

        public static AdViewModel MapToAdViewModel(Ad entity)
        {
            return new AdViewModel()
            {
                ID = entity.ID,
                Name = entity.Name,
                AdDetail = entity.AdDetail,
                Owner = entity.Owner,
                Category = entity.Category,
                City = entity.City,
                Price = entity.Price
            };
        }

        public static List<AdGridViewModel> MapToAdGridModel(List<Ad> entities)
        {
            List<AdGridViewModel> adGrid = new List<AdGridViewModel>();

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    var mainPicture = entity.AdDetail?.AdDetailPictures?.FirstOrDefault()?.File;
                    var description = entity.AdDetail?.Description;

                    adGrid.Add(
                        new AdGridViewModel()
                        {
                            ID = entity.ID,
                            Name = entity.Name,
                            Price = entity.Price,
                            City = entity.City,
                            Category = entity.Category,
                            Owner = entity.Owner,
                            MainPicture = mainPicture,
                            Description = description
                        }
                    );
                }
            }

            return adGrid;
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
                            ID = entity.ID,
                            Name = entity.Name,
                        }
                    );
                }
            }

            return viewModelList;
        }

    }
}
