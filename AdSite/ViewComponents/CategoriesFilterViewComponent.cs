﻿using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.ViewComponents
{
    public class CategoriesFilterViewComponent : ViewComponent
    {
        string COUNTRY_ID = "CountryId";

        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;

        public CategoriesFilterViewComponent(ICategoryService categoryService, ICountryService countryService)
        {
            _categoryService = categoryService;
            _countryService = countryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ICollection<CategoryViewModel> categories, bool isFirstCall, Guid? selectedCategoryId)
        {
            if (isFirstCall)
            {
                Guid countryId = _countryService.Get((Guid)HttpContext.Items[COUNTRY_ID]);

                categories = _categoryService.GetCategoryAsTreeStructure(countryId);
            }

            var viewModel = new CategoryFilterViewModel { IsFirst = isFirstCall, ComponentCategories = categories, SelectedCategoryId = selectedCategoryId };

            return View(viewModel);
        }
    }
}
