using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class AdViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        //todo: property for Main Picture

        public Category Category { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public AdDetail AdDetail { get; set; }
        public ApplicationUser Owner { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        ApplicationUser ModifiedBy { get; set; }
        ApplicationUser CreatedBy { get; set; }
    }
}
