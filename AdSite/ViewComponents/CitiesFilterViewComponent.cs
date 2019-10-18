using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdSite.ViewComponents
{
    public class CitiesFilterViewComponent : ViewComponent
    {
        string COUNTRY_ID = "CountryId";

        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public CitiesFilterViewComponent(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ICollection<LookupViewModel> cities, bool isFirstCall, List<Guid> cityIds)
        {
            cityIds = (cityIds == null) ? new List<Guid>() : cityIds;
            if (isFirstCall)
            {
                Guid countryId = _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);

                cities = _cityService.GetAllAsLookup(countryId);
            }

            var viewModel = new CitiesFilterViewModel() { IsFirst = isFirstCall, ComponentCities = cities, CityIds = cityIds };

            return View(viewModel);
        }
    }
}
