using AdSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdSite.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class LocalizationApiController : ControllerBase
    {
        private readonly ILocalizationService _localizationService;
        public LocalizationApiController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        // GET: api/<LocalizationApiController>
        [HttpGet]
        public string Get()
        {
            return String.Empty;
        }

        // GET api/<LocalizationApiController>/5
        [HttpGet("{localizationKey}")]
        public string Get(string localizationKey)
        {
            return _localizationService.GetByKey(localizationKey, Thread.CurrentThread.CurrentCulture.LCID);
        }

        // POST api/<LocalizationApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LocalizationApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocalizationApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
