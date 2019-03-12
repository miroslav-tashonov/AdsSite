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

        public string LocalizationValue { get; set; }

        public Language Language { get; set; }
    }
}
