using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Models.AccountViewModels
{
    public class ManageAccountModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public Guid CountryId { get; set; }
    }
}
