using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models
{
    public class StampBaseClass
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt
        {
            get => DateTime.Now;
            set { }
        }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt {
            get => DateTime.Now;
            set { }
        }        
        
    }
}
