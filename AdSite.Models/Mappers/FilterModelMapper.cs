using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class FilterModelMapper
    {
        private const int MAX_NUMBER = 1000000;
        public static FilterRepositoryModel MapToFilterRepositoryModel(FilterModel filterModel, List<Guid> categoryIds, Guid countryId)
        {
            if (filterModel.CityIds == null)
                filterModel.CityIds = new List<Guid>();

            int maxPrice = filterModel.MaximumPrice <= 0 ? MAX_NUMBER : filterModel.MaximumPrice ; 

            var filterRespositoryModel = new FilterRepositoryModel()
            {
                CategoryIds = categoryIds,
                CityIds = filterModel.CityIds,
                MinimumPrice = filterModel.MinimumPrice,
                MaximumPrice = maxPrice,
                CountryId = countryId
            };

            return filterRespositoryModel;
        }


    }
}
