using AdSite.Models.CRUDModels.AuditedModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class WishlistViewModel
    {
        public Guid ID { get; set; }

        public WishlistAdGridModel Ad { get; set; }
    }

}
