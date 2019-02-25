using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.AdSiteDomainModels
{
    public class Ad
    {
        public int AdID { get; set; } // primary key

        public string Name { get; set; }
        public decimal Price { get; set; }

        //todo: property for Main Picture

        #region Foreign Keys
        public int CategoryID { get; set; }
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public int AdDetailID { get; set; }

        public Category Category { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public AdDetail AdDetail { get; set; }

        //todo: reference to aspnet users for the owner
        //public User Owner { get; set; }
        #endregion

    }
}
