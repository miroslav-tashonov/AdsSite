using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class CategoryMapper
    {

        public static List<LookupViewModel> MapToLookupViewModel(List<Category> categories)
        {
            var listViewModel = new List<LookupViewModel>();
            if (categories != null && categories.Count > 0)
            {
                foreach (Category category in categories)
                {
                    var viewModel = new LookupViewModel();

                    viewModel.Id = category.ID;
                    viewModel.Name = category.Name;

                    listViewModel.Add(viewModel);
                }
            }
            return listViewModel;
        }

        public static List<CategoryViewModel> MapToViewModel(List<Category> categories)
        {
            var listViewModel = new List<CategoryViewModel>();
            if (categories != null && categories.Count > 0)
            {
                foreach (Category category in categories)
                {
                    var viewModel = new CategoryViewModel();

                    viewModel.ID = category.ID;
                    viewModel.Title = category.Name;
                    viewModel.Type = category.Type;
                    viewModel.ParentId = category.ParentId;
                    viewModel.Children = MapToViewModel(category.Children.ToList());
                    viewModel.ImagePath = category.ImagePath;

                    listViewModel.Add(viewModel);
                }
            }
            return listViewModel;
        }

    }
}
