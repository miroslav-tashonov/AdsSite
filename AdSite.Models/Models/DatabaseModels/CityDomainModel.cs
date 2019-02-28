using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class City : StampBaseClass
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Postcode { get; set; }

        #region Foreign Keys

        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Ad> Ads { get; set; }
        #endregion

    }
}
