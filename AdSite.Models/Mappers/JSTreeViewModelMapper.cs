using AdSite.Models.DatabaseModels;
using AdSite.Models.Models.AdSiteViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Mappers
{
    public class JSTreeViewModelMapper
    {
        public static JSTreeViewModel MapToJSTreeViewModel(Category category)
        {
            string parentCategoryId = String.IsNullOrEmpty(category.ParentId.ToString()) ? "#" : category.ParentId.ToString();

            JSTreeViewModel model = new JSTreeViewModel
            {
                id = category.ID.ToString(),
                text = category.Name,
                parent = parentCategoryId,
                type = category.Type
            };

            return model;
        }

        public static List<JSTreeViewModel> MapToJSTreeViewModel(List<Category> categories)
        {
            List<JSTreeViewModel> model = new List<JSTreeViewModel>();
            foreach (var category in categories)
            {
                model.Add(MapToJSTreeViewModel(category));
            }

            return model;
        }
    }
}
