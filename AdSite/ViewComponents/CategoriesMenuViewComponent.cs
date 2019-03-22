using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.ViewComponents
{
    public class CategoriesMenuViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoriesMenuViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ICollection<Category> categories, bool isFirstCall)
        {
            if (isFirstCall)
            {
                categories = _categoryService.GetCategoryAsTreeStructure();
            }

            var viewModel = new CategoryComponentViewModel { IsFirst = isFirstCall, ComponentCategories = categories };

            return View(viewModel);
        }
    }
}
