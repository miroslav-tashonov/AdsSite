using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class Ad : StampBaseClass
    {
        public Guid AdID { get; set; } // primary key
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        //todo: property for Main Picture

        #region Foreign Keys
        public Guid CategoryID { get; set; }
        public Guid CityID { get; set; }
        public Guid CountryID { get; set; }

        public Category Category { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public AdDetail AdDetail { get; set; }

        public ApplicationUser Owner { get; set; }
        #endregion

    }
}
