using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<UserRoleCountry> UserRoleCountry { get; set; }
        public ICollection<Ad> Ads { get; set; }
    }

    public class ApplicationUserWithToken : ApplicationUser
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
