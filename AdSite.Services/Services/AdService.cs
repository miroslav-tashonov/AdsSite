using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using AdSite.Models.Mappers;
using AdSite.Services.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AdSite.Services
{
    public interface IAdService
    {
        bool Exists(Guid id);
        int Count();
        bool Delete(Guid id);
        bool Add(AdCreateModel ad);
        bool Update(AdEditModel ad);
        List<AdViewModel> GetAds(Guid countryId);
        List<AdProductsModel> GetAdGridModel(Guid countryId);
        List<AdGridViewModel> GetPageForAdGrid(PageModel pageModel, out int count, out int maxPrice);
        List<AdGridViewModel> GetPageForMyAdsGrid(PageModel pageModel, string ownerIdentifier, out int count);
        List<AdGridViewModel> GetPageForAdGridByFilter(PageModel pageModel, FilterModel filterModel , out int count, out int maxPrice);
        AdProductsModel GetAdAsViewModel(Guid adId);
        WishlistAdGridModel GetAdAsAdWishlistGridModel(Guid adId);
        AdEditModel GetAdAsEditModel(Guid adId);
    }



    public class AdService : IAdService
    {
        private readonly IAdRepository _repository;
        private readonly IWishlistRepository _wishlistRepository ;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger _logger;

        private Guid CurrentAdId;
        private const int NUMBER_OF_RELATED_ADS = 9;
        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_AD_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_Ad_Not_Found", CultureId);
        private string LOCALIZATION_GENERAL_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_General_Not_Found", CultureId);

        public AdService(IAdRepository repository, ILocalizationRepository localizationRepository, 
            ICategoryRepository categoryRepository, ICityRepository cityRepository, 
            IWishlistRepository wishlistRepository, ILogger<AdService> logger)
        {
            _wishlistRepository = wishlistRepository;
            _localizationRepository = localizationRepository;
            _categoryRepository = categoryRepository;
            _cityRepository = cityRepository;
            _repository = repository;
            _logger = logger;
        }

        public bool Add(AdCreateModel entity)
        {
            #region Pictures Manipulation
            List<AdDetailPicture> pictures = new List<AdDetailPicture>();
            if (entity.FilesAsListOfByteArray != null && entity.FilesAsListOfByteArray.Count > 0)
            {
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

                entity.MainPictureThumbnail = MagiskImageWrapper.MakeThumbnailImage(System.Convert.FromBase64String(entity.MainPictureFile));
            }
            #endregion

            var ad = AdMapper.MapAdFromAdCreateModel(entity, pictures);

            return _repository.Add(ad);
        }

        public bool Delete(Guid id)
        {
            try
            {
                return _repository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while deleting cities : {0} - {1} ", ex.StackTrace, ex.Message);
                throw ex;
            }
        }


        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public List<AdViewModel> GetAds(Guid countryId)
        {
            var entities = _repository.GetAll(countryId);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdViewModel(entities);
        }


        public AdEditModel GetAdAsEditModel(Guid id)
        {
            var entity = _repository.GetAdWithDetails(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdEditModel(entity);
        }

        public AdProductsModel GetAdAsViewModel(Guid id)
        {
            CurrentAdId = id;
            var entity = _repository.GetAdWithDetails(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdProductsModel(entity);
        }

        public WishlistAdGridModel GetAdAsAdWishlistGridModel(Guid id)
        {
            var entity = _repository.GetAdWithDetails(id);
            if (entity == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToWishlistAdGridModel(entity);
        }

        public List<AdProductsModel> GetAdGridModel(Guid countryId)
        {
            var entities = _repository.GetAdGrid(countryId);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            return AdMapper.MapToAdProductsModel(entities);
        }

        public List<AdGridViewModel> GetPageForAdGrid(PageModel pageModel, out int count, out int maxPrice)
        {
            List<Ad> sourceEntities;
            switch (pageModel.ColumnName.ToLower())
            {
                case "name":
                    sourceEntities = _repository.GetAdGrid(pageModel.SearchString, pageModel.CountryId);
                    break;
                default:
                    sourceEntities = _repository.GetAdGrid(pageModel.CountryId);
                    break;
            }

            if (sourceEntities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }
            sourceEntities = _repository.OrderAdsByColumn(sourceEntities, pageModel.SortColumn);
            maxPrice = (int)_repository.GetMaximumPriceForAd();
            count = sourceEntities.Count;

            var entities = _repository.GetAdPage(sourceEntities, pageModel.PageIndex, pageModel.PageSize);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            var list = AdMapper.MapToAdGridModel(entities);

            foreach (var ad in list)
            {
                ad.IsInWishlist = _wishlistRepository.Exists(ad.ID, pageModel.CurrentUser);
            }

            return list;
        }

        public List<AdGridViewModel> GetPageForMyAdsGrid(PageModel pageModel, string ownerIdentifier, out int count)
        {
            List<Ad> sourceEntities;
            switch (pageModel.ColumnName.ToLower())
            {
                case "name":
                    sourceEntities = _repository.GetMyAdsGrid(pageModel.SearchString, ownerIdentifier, pageModel.CountryId);
                    break;
                default:
                    sourceEntities = _repository.GetMyAdsGrid(String.Empty, ownerIdentifier, pageModel.CountryId);
                    break;
            }

            if (sourceEntities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }
            sourceEntities = _repository.OrderAdsByColumn(sourceEntities, pageModel.SortColumn);
            count = sourceEntities.Count;

            var entities = _repository.GetAdPage(sourceEntities, pageModel.PageIndex, pageModel.PageSize);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            var list = AdMapper.MapToAdGridModel(entities);

            foreach (var ad in list)
            {
                ad.IsInWishlist = _wishlistRepository.Exists(ad.ID, pageModel.CurrentUser);
            }

            return list;
        }


        public List<AdGridViewModel> GetPageForAdGridByFilter(PageModel pageModel, FilterModel filterModel, out int count, out int maxPrice)
        {
            //business rule : retrieve subcategories for current category and apply to filtering 
            List<Guid> categoryIds = new List<Guid>();
            if (filterModel.CategoryId != null && filterModel.CategoryId != Guid.Empty)
            {
                categoryIds = _categoryRepository.GetSubcategoriesIdForCategory((Guid)filterModel.CategoryId, pageModel.CountryId);
            }

            FilterRepositoryModel repositoryModel = FilterModelMapper.MapToFilterRepositoryModel(filterModel, categoryIds, pageModel.CountryId);
            List<Ad> sourceEntities;
            switch (pageModel.ColumnName.ToLower())
            {
                case "name":
                    sourceEntities = _repository.GetAdGridByFilters(pageModel.SearchString, repositoryModel);
                    break;
                default:
                    sourceEntities = _repository.GetAdGridByFilters(repositoryModel);
                    break;
            }

            if (sourceEntities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }
            sourceEntities = _repository.OrderAdsByColumn(sourceEntities, pageModel.SortColumn);
            maxPrice = (int)_repository.GetMaximumPriceForAd();
            count = sourceEntities.Count;

            var entities = _repository.GetAdPage(sourceEntities, pageModel.PageIndex, pageModel.PageSize);
            if (entities == null)
            {
                throw new Exception(LOCALIZATION_AD_NOT_FOUND);
            }

            var list = AdMapper.MapToAdGridModel(entities);
            foreach(var ad in list)
            {
                ad.IsInWishlist = _wishlistRepository.Exists(ad.ID, pageModel.CurrentUser);
            }
            return list;
        }

        public bool Update(AdEditModel entity)
        {
            Ad ad = _repository.GetAdWithDetails(entity.ID);
            if (ad == null)
            {
                throw new Exception(LOCALIZATION_GENERAL_NOT_FOUND + entity.ID);
            }

            if (entity.AdDetail != null && entity.AdDetail.AdDetailPictures != null)
                foreach (var adDetailMap in entity.AdDetail.AdDetailPictures)
                {
                    ad.AdDetail.AdDetailPictures.Remove(adDetailMap);
                }

            entity.MainPictureThumbnail = MagiskImageWrapper.MakeThumbnailImage(System.Convert.FromBase64String(entity.MainPictureFile));
            ad = AdMapper.MapAdFromAdEditModel(entity, ad);

            return _repository.Update(ad);
        }

        #region Helper Methods
        private List<AdGridViewModel> LoadRelatedAdsForCategories(Guid currentCategoryId)
        {
            var relatedAds = RecursivelyLoadRelatedAdsForCategory(currentCategoryId);

            return AdMapper.MapToAdGridModel(relatedAds);
        }

        //if there arent enough ads in current category, get ads from parent category
        private List<Ad> RecursivelyLoadRelatedAdsForCategory( Guid categoryId)
        {
            var relatedAds = _repository.GetRelatedAdsForCategoryExceptCurrentAd(CurrentAdId, categoryId);
            if(relatedAds != null && relatedAds.Count < NUMBER_OF_RELATED_ADS)
            {
                Guid? parentCategoryId = _categoryRepository.GetCategoryParentId(categoryId);
                if (parentCategoryId != null && parentCategoryId != Guid.Empty)
                {
                    relatedAds.AddRange( RecursivelyLoadRelatedAdsForCategory((Guid)parentCategoryId) );
                }
            }
            return relatedAds;
        }


#endregion


    }
}
