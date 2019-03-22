using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class AuditedEntityViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public LookupViewModel ModifiedBy { get; set; }
        public LookupViewModel CreatedBy { get; set; }
        public LookupViewModel Country { get; set; }
    }

    public class AuditedEntityModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public Guid CountryId { get; set; }
    }

}
