using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdSite.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesApiController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesApiController(ICityService cityService)
        {
            _cityService = cityService;
        }


        // GET api/<CitiesApiController>/5
        [HttpGet("{countryId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        public IActionResult Get(Guid countryId)
        {
            return Ok(_cityService.GetCities(countryId));
        }

        // POST api/<CitiesApiController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        public void Post([FromBody]CityCreateModel model)
        {
            model.CreatedAt = DateTime.Now;
            model.ModifiedAt = DateTime.Now;
            model.CreatedBy = "mtashonov";
            model.ModifiedBy = "mtashonov";

            Ok(_cityService.Add(model));
        }

        // PUT api/<CitiesApiController>/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        public void Put([FromBody]CityEditModel model)
        {
            model.CreatedAt = DateTime.Now;
            model.ModifiedAt = DateTime.Now;
            model.CreatedBy = "mtashonov";
            model.ModifiedBy = "mtashonov";

            Ok(_cityService.Update(model));
        }

        // DELETE api/<CitiesApiController>/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_cityService.Delete(id));
        }
    }
}
