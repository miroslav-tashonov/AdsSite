using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class Localization
    {
        public Guid LocalizationID { get; set; } // primary key
        [Required]
        public string LocalizationKey { get; set; }

        public string English { get; set; }
        public string Macedonian { get; set; }
        public string Albanian { get; set; }
    }
}
