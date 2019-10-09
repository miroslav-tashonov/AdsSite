using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class UserRoleCountryGridModel
    {
        public string ApplicationUserId { get; set; }
        public Guid CountryId { get; set; }
        public string RoleId { get; set; }
    }


    public class UserRoleCountryCreateModel
    {
        public string ApplicationUserId { get; set; }
        public Guid CountryId { get; set; }
        public string RoleId { get; set; }
    }

    public class UserRoleCountryEditModel
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public Guid CountryId { get; set; }
        public string RoleId { get; set; }
    }
}
