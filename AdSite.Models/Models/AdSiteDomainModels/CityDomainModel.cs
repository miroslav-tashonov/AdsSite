using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.AdSiteDomainModels
{
    public class City
    {
        public int CityID { get; set; } // primary key

        public string Name { get; set; }
        public string Postcode { get; set; }

        //property for Main Picture

        #region Foreign Keys

        public int CountryId { get; set; }

        public Country Country { get; set; }
        public ICollection<Ad> Ads { get; set; }
        #endregion

    }
}
