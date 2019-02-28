using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models
{
    public class ApplicationIdentityRole : IdentityRole
    {
        public ICollection<UserRoleCountry> UserRoleCountry { get; set; }
    }
}
