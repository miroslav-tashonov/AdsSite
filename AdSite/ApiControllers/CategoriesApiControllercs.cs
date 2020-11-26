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
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;

        public CategoriesApiController(ICountryService countryService, ICategoryService categoryService)
        {
            _countryService = countryService;
            _categoryService = categoryService;
        }

        [HttpGet("{countryId}")]
        public IEnumerable<CategoryViewModel> Get(Guid countryId)
        {
            return _categoryService.GetCategoryAsTreeStructure(countryId);
        }
    }
}
