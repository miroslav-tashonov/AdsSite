using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.ViewModels
{
    public class CountryViewModel
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        
        public ICollection<City> Cities { get; set; }
        public ICollection<Ad> Ads { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<UserRoleCountry> UserRoleCountry{ get; set; }
        public ICollection<Localization> Localizations { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        ApplicationUser ModifiedBy { get; set; }
        ApplicationUser CreatedBy { get; set; }
    }
}
