using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class Country : StampBaseClass
    {
        public Guid CountryID { get; set; } // primary key
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }

        #region Foreign Keys
        public ICollection<City> Cities { get; set; }
        public ICollection<Ad> Ads { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<UserRoleCountry> UserRoleCountry{ get; set; }
        public ICollection<Localization> Localizations { get; set; }
        #endregion

    }
}
