using AdSite.Models.CRUDModels.AuditedModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class CountryViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Path { get; set; }
        
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

    public class CountryCreateModel : AuditedEntityModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        [Required]
        public string Path { get; set; }
    }
    public class CountryEditModel : AuditedEntityModel
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }
        [Required]
        public string Path { get; set; }
    }

    public class CountryModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Path { get; set; }
    }
}
