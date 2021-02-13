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
        public string Address { get; set; }
        public string Title { get; set; }
        public string LogoImagePath { get; set; }

        public string FacebookSocialLink { get; set; }
        public string InstagramSocialLink { get; set; }
        public string TwitterSocialLink { get; set; }
        public string GooglePlusSocialLink { get; set; }
        public string VKSocialLink { get; set; }
        
        public Guid CountryId { get; set; }
    }


    public class WebSettingsEditModel 
    {
        public Guid ID { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LogoImagePath { get; set; }

        public string FacebookSocialLink { get; set; }
        public string InstagramSocialLink { get; set; }
        public string TwitterSocialLink { get; set; }
        public string GooglePlusSocialLink { get; set; }
        public string VKSocialLink { get; set; }

        [Required]
        public Guid CountryId { get; set; }
    }

    public class WebSettingsCreateModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LogoImagePath { get; set; }

        public string FacebookSocialLink { get; set; }
        public string InstagramSocialLink { get; set; }
        public string TwitterSocialLink { get; set; }
        public string GooglePlusSocialLink { get; set; }
        public string VKSocialLink { get; set; }

        [Required]
        public Guid CountryId { get; set; }
    }
}
