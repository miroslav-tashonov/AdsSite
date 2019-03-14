using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.ViewModels
{
    public class LookupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
