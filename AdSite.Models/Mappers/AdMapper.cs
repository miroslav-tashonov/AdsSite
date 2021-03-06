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

            var mainPicture = entity.AdDetail?.MainPictureThumbnailFile;
            var description = entity.AdDetail?.Description;

            editModel.ID = entity.ID;
            editModel.Name = entity.Name;
            editModel.OwnerId = entity.OwnerId;
            editModel.Price = entity.Price;
            editModel.CategoryId = entity.CategoryID;
            editModel.CityId = entity.CityID;
            editModel.AdDetail.AdDetailPictures = entity.AdDetail?.AdDetailPictures;
            editModel.MainPictureThumbnail = mainPicture;
            editModel.Description = description;

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

        public static AdProductsModel MapToAdProductsModel(Ad ad)
        {
            return new AdProductsModel()
            {
                id = ad.ID,
                name = ad.Name,
                price = ad.Price,
                salePrice = ad.Price,
                discount = 50,
                shortDetails = ad.AdDetail?.Description,
                description = ad.AdDetail?.Description,
                category = ad.Category?.Name,
                tags = new List<string> { ad.City?.Name },
                pictures = ad.AdDetail?.AdDetailPictures?.Select(x => x.File).ToList(),
                createdAt = ad.CreatedAt
            };
        }

        public static Ad MapAdFromAdCreateModel(AdCreateModel entity, List<AdDetailPicture> pictures)
        {
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
                    MainPictureThumbnailFile = entity.MainPictureThumbnail,
                    Description = entity.AdDetail.Description,
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
            existingAd.AdDetail.MainPictureThumbnailFile = entity.MainPictureThumbnail;
            existingAd.AdDetail.Description = entity.Description;

            return existingAd;
        }


        public static List<AdGridViewModel> MapToAdGridModel(List<Ad> entities)
        {
            List<AdGridViewModel> adGrid = new List<AdGridViewModel>();

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    var mainPicture = entity.AdDetail?.MainPictureThumbnailFile;
                    var description = entity.AdDetail?.Description;
                    var pictures = entity.AdDetail?.AdDetailPictures?.Select(x => x.File).ToList();

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
                            Description = description,
                            PictureFiles = pictures,
                            CreatedAt = entity.CreatedAt
                        }
                    );
                }
            }

            return adGrid;
        }


        public static List<AdProductsModel> MapToAdProductsModel(List<Ad> entities)
        {
            List<AdProductsModel> adGrid = new List<AdProductsModel>();

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    var description = entity.AdDetail?.Description;
                    var pictures = entity.AdDetail?.AdDetailPictures?.Select(x => x.File).ToList();

                    List<byte[]> picturesArray = new List<byte[]>();
                    if (pictures != null && pictures.Count > 0)
                    {
                        picturesArray.Add(pictures[0]);
                    }

                    adGrid.Add(new AdProductsModel
                    {
                        id = entity.ID,
                        name = entity.Name,
                        price = entity.Price,
                        salePrice = entity.Price,
                        discount = 50,
                        shortDetails = description,
                        description = description,
                        category = entity.Category?.Name,
                        tags = new List<string> { entity.City?.Name },
                        //pictures = ad.PictureFiles,
                        pictures = picturesArray,
                        createdAt = entity.CreatedAt
                    });
                }
            }

            return adGrid;
        }


        public static WishlistAdGridModel MapToWishlistAdGridModel(Ad entity)
        {
            var mainPicture = entity.AdDetail?.MainPictureThumbnailFile;

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
