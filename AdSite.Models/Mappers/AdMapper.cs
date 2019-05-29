﻿using AdSite.Models.CRUDModels;
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
            editModel.AdDetail.AdDetailPictures = entity.AdDetail.AdDetailPictures;

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
                Price = entity.Price,
                CreatedAt = entity.CreatedAt
            };
        }

        public static Ad MapAdFromAdCreateModel(AdCreateModel entity)
        {
            List<AdDetailPicture> pictures = new List<AdDetailPicture>();
            if (entity.FilesAsListOfByteArray != null && entity.FilesAsListOfByteArray.Count > 0)
                foreach (var file in entity.FilesAsListOfByteArray)
                {
                    pictures.Add(new AdDetailPicture
                    {
                        File = file,
                        CreatedBy = entity.CreatedBy,
                        CreatedAt = entity.CreatedAt,
                        ModifiedAt = entity.ModifiedAt,
                        ModifiedBy = entity.ModifiedBy,
                    });
                }

            Ad ad = new Ad
            {
                Name = entity.Name,
                Price = entity.Price,
                CategoryID = entity.CategoryId,
                CityID = entity.CityId,
                OwnerId = entity.OwnerId,
                CreatedBy = entity.CreatedBy,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = entity.ModifiedAt,
                ModifiedBy = entity.ModifiedBy,
                CountryID = entity.CountryId,
                AdDetail = new AdDetail
                {
                    Description = entity.Description,
                    CreatedBy = entity.CreatedBy,
                    CreatedAt = entity.CreatedAt,
                    ModifiedAt = entity.ModifiedAt,
                    ModifiedBy = entity.ModifiedBy,
                    AdDetailPictures = pictures
                }
            };

            return ad;
        }

        public static Ad MapAdFromAdEditModel(AdEditModel entity, Ad existingAd)
        {
            List<AdDetailPicture> pictures = new List<AdDetailPicture>();
            if (entity.FilesAsListOfByteArray != null && entity.FilesAsListOfByteArray.Count > 0)
                foreach (var file in entity.FilesAsListOfByteArray)
                {
                    pictures.Add(new AdDetailPicture
                    {
                        File = file,
                        CreatedBy = entity.CreatedBy,
                        CreatedAt = entity.CreatedAt,
                        ModifiedAt = entity.ModifiedAt,
                        ModifiedBy = entity.ModifiedBy,
                    });
                }

            existingAd.OwnerId = entity.OwnerId;
            existingAd.CityID = entity.CityId;
            existingAd.CategoryID = entity.CategoryId;

            existingAd.ModifiedAt = entity.ModifiedAt;
            existingAd.ModifiedBy = entity.ModifiedBy;

            existingAd.Price = entity.Price;
            existingAd.Name = entity.Name;
            existingAd.ModifiedAt = entity.ModifiedAt;
            existingAd.ModifiedBy = entity.ModifiedBy;

            existingAd.AdDetail.ModifiedAt = entity.ModifiedAt;
            existingAd.AdDetail.ModifiedBy = entity.ModifiedBy;
            existingAd.AdDetail.AdDetailPictures = pictures;

            return existingAd;
        }


        public static List<AdGridViewModel> MapToAdGridModel(List<Ad> entities)
        {
            List<AdGridViewModel> adGrid = new List<AdGridViewModel>();

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    //todo: should the user select the main picture ?
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


        public static WishlistAdGridModel MapToWishlistAdGridModel(Ad entity)
        {
            var mainPicture = entity.AdDetail?.AdDetailPictures?.FirstOrDefault()?.File;

            WishlistAdGridModel adGrid =
                new WishlistAdGridModel()
                {
                    ID = entity.ID,
                    Name = entity.Name,
                    Price = entity.Price,
                    MainPicture = mainPicture
                };

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
