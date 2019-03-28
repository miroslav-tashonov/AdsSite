using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class WebSettingsViewModel
    {
        public Guid ID { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string FacebookSocialLink { get; set; }
        public string InstagramSocialLink { get; set; }
        public string TwitterSocialLink { get; set; }
        public string GooglePlusSocialLink { get; set; }
        public string VKSocialLink { get; set; }
        
        public Guid CountryId { get; set; }
    }


    public class WebSettingsEditModel 
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public string FacebookSocialLink { get; set; }
        public string InstagramSocialLink { get; set; }
        public string TwitterSocialLink { get; set; }
        public string GooglePlusSocialLink { get; set; }
        public string VKSocialLink { get; set; }
    }

    public class WebSettingsCreateModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public string FacebookSocialLink { get; set; }
        public string InstagramSocialLink { get; set; }
        public string TwitterSocialLink { get; set; }
        public string GooglePlusSocialLink { get; set; }
        public string VKSocialLink { get; set; }
    }
}
