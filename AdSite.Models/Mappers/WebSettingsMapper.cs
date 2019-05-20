using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.Mappers
{
    public static class WebSettingsMapper
    {
        public static WebSettingsViewModel MapToWebSettingsViewModel(WebSettings entity)
        {
            return new WebSettingsViewModel()
            {
                ID = entity.ID,
                Phone = entity.Phone,
                Email = entity.Email,
                FacebookSocialLink = entity.FacebookSocialLink,
                InstagramSocialLink = entity.InstagramSocialLink,
                TwitterSocialLink = entity.TwitterSocialLink,
                GooglePlusSocialLink = entity.GooglePlusSocialLink,
                VKSocialLink = entity.VKSocialLink,
                CountryId = entity.CountryId,
            };
        }

        public static WebSettingsEditModel MapToWebSettingsEditModel(WebSettings entity)
        {
            return new WebSettingsEditModel()
            {
                ID = entity.ID,
                Phone = entity.Phone,
                Email = entity.Email,
                FacebookSocialLink = entity.FacebookSocialLink,
                InstagramSocialLink = entity.InstagramSocialLink,
                TwitterSocialLink = entity.TwitterSocialLink,
                GooglePlusSocialLink = entity.GooglePlusSocialLink,
                VKSocialLink = entity.VKSocialLink
            };
        }

        public static WebSettingsCreateModel MapToWebSettingsCreateModel(WebSettings entity)
        {
            return new WebSettingsCreateModel()
            {
                Phone = entity.Phone,
                Email = entity.Email,
                FacebookSocialLink = entity.FacebookSocialLink,
                InstagramSocialLink = entity.InstagramSocialLink,
                TwitterSocialLink = entity.TwitterSocialLink,
                GooglePlusSocialLink = entity.GooglePlusSocialLink,
                VKSocialLink = entity.VKSocialLink
            };
        }

    }
}
