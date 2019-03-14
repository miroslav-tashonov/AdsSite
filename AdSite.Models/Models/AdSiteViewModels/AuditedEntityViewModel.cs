using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.ViewModels
{
    public class AuditedEntityViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public LookupViewModel ModifiedBy { get; set; }
        public LookupViewModel CreatedBy { get; set; }
        public LookupViewModel Country { get; set; }
    }
}
