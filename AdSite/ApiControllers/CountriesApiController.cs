﻿using AdSite.Models.CRUDModels;
using AdSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesApiController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountriesApiController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost, Route("getCountryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult GetCountryId([FromBody]CountryViewModel model)
        {
            var country = _countryService.GetByCountryPath(model.Path.Replace("/", String.Empty));
            if (country == null)
                return BadRequest("Country not found");

            return Ok(new CountryModel { ID = country.ID, Abbreviation = country.Abbreviation, Name = country.Name, Path = country.Path });
        }

    }
}
