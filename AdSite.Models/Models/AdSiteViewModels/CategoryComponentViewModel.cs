using AdSite.Models.DatabaseModels;
using AdSite.Models.Models.AdSiteViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.ViewModels
{
    public class CategoryComponentViewModel 
    {
        public CategoryComponentViewModel()
        {
            ComponentCategories = new List<Category>();
        }

        public bool IsFirst{ get; set; }
        public ICollection<Category> ComponentCategories { get; set; }

    }

    public class CategoryFilterComponentViewModel
    {
        public CategoryFilterComponentViewModel()
        {
            ComponentCategories = new List<JSTreeViewModel>();
        }
        public ICollection<JSTreeViewModel> ComponentCategories { get; set; }

    }
}
