using System;

namespace AdSite.TagHelpers
{
    using AdSite.Services;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Memory;
    using System.Text;
    using System.Threading;

    [HtmlTargetElement("localize")]
    public class LocalizationTagHelper : TagHelper
    {
        private readonly ILocalizationService _localizationService;
        private IMemoryCache _cache;
        public LocalizationTagHelper(ILocalizationService localizationService, IMemoryCache memoryCache)
        {
            _localizationService = localizationService;
            _cache = memoryCache;
        }

        public string Name { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            int currentCultureId = Thread.CurrentThread.CurrentCulture.LCID;
            string entryKey = this.Name + currentCultureId.ToString();
            string localizationValue = String.Empty;
            var sb = new StringBuilder();

            if (!_cache.TryGetValue(entryKey, out localizationValue))
            {
                localizationValue = _localizationService.GetByKey(this.Name, currentCultureId);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(31));
                _cache.Set(entryKey, localizationValue, cacheEntryOptions);
            }

            output.TagName = "LocalizeTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            sb.AppendFormat("{0}", string.IsNullOrEmpty(localizationValue) ? this.Name : localizationValue);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

}
