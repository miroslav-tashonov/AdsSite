using AdSite.Models.CRUDModels;
using AdSite.Services;
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
    public class AdsApiController : ControllerBase
    {
        private readonly IAdService _adService;

        public AdsApiController(IAdService adService)
        {
            _adService = adService;
        }

        // GET: api/<CitiesApiController>
        [HttpPost]
        public IActionResult Get([FromBody] AdGetWithCountryModel model)
        {
            return Ok(_adService.GetAdGridModel(model.countryId));
        }


        [HttpPost, Route("getLatestAds")]
        public IActionResult GetLatestAds([FromBody] AdGetWithCountryModel model)
        {
            return Ok(_adService.GetAdGridModel(model.countryId).OrderByDescending(x => x.createdAt).Take(9));
        }

        [HttpPost, Route("getRelatedAds")]
        public IActionResult GetRelatedAds([FromBody] AdGetWithCountryModel model)
        {
            return Ok(_adService.GetAdGridModel(model.countryId));
        }
        
        [HttpPost("{id}"), Route("getProductDetails")]
        public IActionResult GetProductDetails([FromBody]AdGetModel model)
        {
            return Ok(_adService.GetAdAsViewModel(model.id));
        }
    }

}
