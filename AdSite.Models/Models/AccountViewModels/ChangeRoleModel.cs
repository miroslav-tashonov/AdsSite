using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.AccountViewModels
{
    public class ChangeRoleModel
    {
        [Required]
        public string Id{ get; set; }

        [Required]
        public List<string> Roles { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
