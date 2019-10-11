using AdSite.Models.DatabaseModels;
using AdSite.Models.Models.AdSiteViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class CategoryComponentViewModel 
    {
        public CategoryComponentViewModel()
        {
            ComponentCategories = new List<CategoryViewModel>();
        }

        public bool IsFirst{ get; set; }
        public ICollection<CategoryViewModel> ComponentCategories { get; set; }
    }

    public class CategoryFilterViewModel
    {
        public CategoryFilterViewModel()
        {
            ComponentCategories = new List<CategoryViewModel>();
        }

        public bool IsFirst { get; set; }
        public ICollection<CategoryViewModel> ComponentCategories { get; set; }

        public Guid? SelectedCategoryId { get; set; }
    }


    public class CategoryTreeViewModel
    {
        public CategoryTreeViewModel()
        {
            CategoriesAsTree = new List<JSTreeViewModel>();
        }

        public string PathString { get; set; }
        public ICollection<JSTreeViewModel> CategoriesAsTree { get; set; }
    }

}
