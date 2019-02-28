using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class UserRoleCountry
    {
        public Guid Id { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Guid CountryId { get; set; }
        public Country Country { get; set; }

        public string ApplicationIdentityRoleId { get; set; }
        public ApplicationIdentityRole ApplicationIdentityRole { get; set; }
    }
}
