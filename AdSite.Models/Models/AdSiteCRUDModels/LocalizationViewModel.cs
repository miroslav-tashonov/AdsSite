using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class LocalizationViewModel
    {
        public string LocalizationKey { get; set; }

        public string English { get; set; }
        public string Macedonian { get; set; }
        public string Albanian { get; set; }
    }
}
