using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class Localization : RepositoryEntity
    {
        [Required]
        public string LocalizationKey { get; set; }
        [Required]
        public string LocalizationValue { get; set; }
        [Required]
        public Language Language { get; set; }
        public Guid LanguageId { get; set; }

        public Country Country{ get; set; }
        public Guid CountryId { get; set; }

    }
}
