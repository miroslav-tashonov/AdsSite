using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class UserRoleCountryViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public Country Country { get; set; }
        public ApplicationIdentityRole ApplicationIdentityRole { get; set; }
    }
}
