using AdSite.Models.DatabaseModels;
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
            CompenentCategories = new List<Category>();
        }

        public bool IsFirst{ get; set; }
        public ICollection<Category> CompenentCategories { get; set; }

    }
}
