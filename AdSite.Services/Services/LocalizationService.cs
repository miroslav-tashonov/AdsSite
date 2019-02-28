using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Services.LocalizationService
{
    public interface ILocalizationService
    {
        string Get(Guid countryId, string localizationKey);
    }

    public class LocalizationService : ILocalizationService
    {
        public string Get(Guid countryId, string localizationKey)
        {
            return "Tashonov";
        }
    }
}
