using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.TagHelpers
{
    using AdSite.Services.LocalizationService;
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

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromDays(31));

                // Save data in cache.
                _cache.Set(entryKey, localizationValue, cacheEntryOptions);
            }


            output.TagName = "LocalizeTagHelper";
            output.TagMode = TagMode.StartTagAndEndTag;

            sb.AppendFormat("{0}", string.IsNullOrEmpty(localizationValue) ? this.Name : localizationValue);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }

}
