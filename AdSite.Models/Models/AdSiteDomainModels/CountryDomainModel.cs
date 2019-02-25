using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.AdSiteDomainModels
{
    public class Country
    {
        public int CountryID { get; set; } // primary key
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        #region Foreign Keys
        public ICollection<City> Cities { get; set; }
        public ICollection<Ad> Ads { get; set; }
        public ICollection<Category> Categories { get; set; }
        //todo: property for users and roles
        #endregion

    }
}
