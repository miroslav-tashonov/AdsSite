using AdSite.Models.DatabaseModels;
using AdSite.Models.Models.AdSiteViewModels;
using AdSite.Models.ViewModels;
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
        private readonly ICategoryService _categoryService;
        
        public CategoriesFilterViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _categoryService.GetBlogCategoryTree();

            var mappedJSTreeCategories = JSTreeViewModel.MapToJSTreeViewModel(categories);

            var viewModel = new CategoryFilterComponentViewModel { ComponentCategories = mappedJSTreeCategories };



            return View(viewModel);
        }

    
    }
}
