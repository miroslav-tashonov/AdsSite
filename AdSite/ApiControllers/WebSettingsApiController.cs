using AdSite.Mappers;
using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSettingsApiController : ControllerBase
    {
        private readonly IWebSettingsService _webSettingsService;
        public WebSettingsApiController(IWebSettingsService webSettingsService)
        {
            _webSettingsService = webSettingsService;
        }

        [HttpGet("{countryId}")]
        public WebSettingsViewModel Get(Guid countryId)
        {
            return _webSettingsService.GetWebSettingsViewModelForCountry(countryId);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        public void Put([FromBody]WebSettingsEditModel model)
        {
            Ok(_webSettingsService.UpdateWebSettingsForCountry(model, model.CountryId));
        }
    }
}
