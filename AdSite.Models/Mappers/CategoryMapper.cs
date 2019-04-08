using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
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


    }
}
