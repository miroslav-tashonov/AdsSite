using AdSite.Models.CRUDModels.AuditedModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class CityViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }

        
        public Country Country { get; set; }
        public ICollection<Ad> Ads { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        ApplicationUser ModifiedBy { get; set; }
        ApplicationUser CreatedBy { get; set; }
    }

    public class CityCreateModel : AuditedEntityModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Postcode { get; set; }
    }
    public class CityEditModel : AuditedEntityModel
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Postcode { get; set; }
    }

}
