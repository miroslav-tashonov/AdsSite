using AdSite.Models.CRUDModels.AuditedModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class LocalizationViewModel
    {
        public Guid Id { get; set; }
        public string LocalizationKey { get; set; }
        public string LocalizationValue { get; set; }
        public LookupViewModel Language { get; set; }
    }

    public class LocalizationCreateModel : AuditedCountryEntityModel
    {
        [Required]
        public string LocalizationKey { get; set; }
        [Required]
        public string LocalizationValue { get; set; }
        [Required]
        public Guid LanguageId { get; set; }
    }

    public class LocalizationEditModel 
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string LocalizationKey { get; set; }
        [Required]
        public string LocalizationValue { get; set; }
        [Required]
        public Guid LanguageId { get; set; }
    }

}
