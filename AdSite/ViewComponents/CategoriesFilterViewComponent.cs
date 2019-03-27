using AdSite.Models.DatabaseModels;
using AdSite.Models.Models.AdSiteViewModels;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdSite.Mappers;

namespace AdSite.ViewComponents
{
    public class CategoriesFilterViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        public CategoriesFilterViewComponent(ICategoryService categoryService, ICountryService countryService)
        {
            _categoryService = categoryService;
            _countryService = countryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid countryId = _countryService.Get();

            var categories = _categoryService.GetCategoryTree(countryId);
            var mappedJSTreeCategories = JSTreeViewModelMapper.MapToJSTreeViewModel(categories);
            var viewModel = new CategoryFilterComponentViewModel { ComponentCategories = mappedJSTreeCategories };

            return View(viewModel);
        }
    }
}
