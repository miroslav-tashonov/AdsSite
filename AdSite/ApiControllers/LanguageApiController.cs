using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdSite.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageApiController : ControllerBase
    {
        IOptions<RequestLocalizationOptions> _locOptions;
        public LanguageApiController(IOptions<RequestLocalizationOptions> locOptions)
        {
            _locOptions = locOptions;
        }


        [HttpGet("{countryId}"), Route("getCountryId")]
        public List<LanguageSelectViewModel> GetSupportedCultures()
        {
            return _locOptions.Value.SupportedUICultures
                .Select(c => new LanguageSelectViewModel { LanguageShortName = c.Name, LanguageName = c.DisplayName, CultureId = c.LCID })
                .ToList();
        }
    }
}
