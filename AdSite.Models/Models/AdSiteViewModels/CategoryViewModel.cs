using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        
        public Country Country { get; set; }
        public ICollection<Ad> Ads { get; set; }
        
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        
        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        ApplicationUser ModifiedBy { get; set; }
        ApplicationUser CreatedBy { get; set; }

    }
}
