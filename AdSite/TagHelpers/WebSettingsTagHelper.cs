using System;

namespace AdSite.TagHelpers
{
    using AdSite.Services;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Memory;
    using System.Text;

    [HtmlTargetElement("webSettings")]
    public class WebSettingsTagHelper : TagHelper
    {
        private readonly IWebSettingsService _webSettingsService;
        private readonly ICountryService _countryService;
        private IMemoryCache _cache;

        private Guid CountryId => _countryService.Get();

        public WebSettingsTagHelper(IWebSettingsService webSettingsService, ICountryService countryService, IMemoryCache memoryCache)
        {
            _webSettingsService = webSettingsService;
            _countryService = countryService;
            _cache = memoryCache;
        }

        public string SettingName { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string entryKey = this.SettingName;
            string webSettingsValue = String.Empty;
            var sb = new StringBuilder();

            if (!_cache.TryGetValue(entryKey, out webSettingsValue))
            {
                var webSettings = _webSettingsService.GetWebSettingsViewModelForCountry(CountryId);
                switch (entryKey.ToLower())
                {
                    case "phone":
                        webSettingsValue = webSettings.Phone;
                        break;
                    case "email":
                        webSettingsValue = webSettings.Email;
                        break;
                    case "facebook":
                        webSettingsValue = webSettings.FacebookSocialLink;
                        break;
                    case "instagram":
                        webSettingsValue = webSettings.InstagramSocialLink;
                        break;
                    case "googleplus":
                        webSettingsValue = webSettings.GooglePlusSocialLink;
                        break;
                    case "vk":
                        webSettingsValue = webSettings.VKSocialLink;
                        break;
                    case "twitter":
                        webSettingsValue = webSettings.TwitterSocialLink;
                        break;
                }


                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(31));
                _cache.Set(entryKey, webSettingsValue, cacheEntryOptions);
            }

            output.TagName = "WebSettingsTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            sb.AppendFormat("{0}", string.IsNullOrEmpty(webSettingsValue) ? String.Empty : webSettingsValue);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

}
