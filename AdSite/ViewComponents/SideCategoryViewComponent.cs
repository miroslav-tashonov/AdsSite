using AdSite.Models.DatabaseModels;
using AdSite.Models.ViewModels;
using AdSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ICollection<Category> categories, bool isFirstCall)
        {
            if (isFirstCall)
            {
                categories = _categoryService.GetBlogCategoryTree();
            }

            var viewModel = new CategoryComponentViewModel { IsFirst = isFirstCall, CompenentCategories = categories };

            return View(viewModel);
        }
    }
}
