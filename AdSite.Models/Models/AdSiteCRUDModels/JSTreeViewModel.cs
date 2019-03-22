using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdSite.Models.Models.AdSiteViewModels
{
    public class JSTreeViewModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }
}
