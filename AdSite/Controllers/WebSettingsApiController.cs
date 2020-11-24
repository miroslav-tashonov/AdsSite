using AdSite.Models.CRUDModels;
using AdSite.Services;
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

        // GET: api/<WebSettingsApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WebSettingsApiController>/5
        [HttpGet("{countryId}")]
        public WebSettingsViewModel Get(string countryId)
        {
            return _webSettingsService.GetWebSettingsViewModelForCountry(new Guid(countryId));
        }

        // POST api/<WebSettingsApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WebSettingsApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WebSettingsApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
