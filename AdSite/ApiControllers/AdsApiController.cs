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
        [HttpGet]
        public IActionResult Get()
        {
            List<AdProductsModel> list = new List<AdProductsModel>();

            var adsArray = _adService.GetAdGridModel(new Guid("99DE8181-09A8-41DB-895E-54E5E0650C3A"));
            foreach (var ad in adsArray)
            {
                List<byte[]> pictures = new List<byte[]>();
                if(ad.PictureFiles != null && ad.PictureFiles.Count > 0)
                {
                    pictures.Add(ad.PictureFiles[0]); 
                }

                list.Add(new AdProductsModel
                {
                    id = ad.ID,
                    name = ad.Name,
                    price = ad.Price,
                    salePrice = ad.Price,
                    discount = 50,
                    shortDetails = ad.Description,
                    description = ad.Description,
                    category = ad.Category?.Name,
                    tags = new List<string> { ad.City?.Name },
                    //pictures = ad.PictureFiles,
                    pictures = pictures,
                    createdAt = ad.CreatedAt
                });
            }

            return Ok(list);
        }


        [HttpGet, Route("getLatestAds")]
        public IActionResult GetLatestAds()
        {
            List<AdProductsModel> list = new List<AdProductsModel>();

            var adsArray = _adService.GetAdGridModel(new Guid("99DE8181-09A8-41DB-895E-54E5E0650C3A"))
                .OrderByDescending(x => x.CreatedAt)
                .Take(9);

            foreach (var ad in adsArray)
            {
                List<byte[]> pictures = new List<byte[]>();
                if (ad.PictureFiles != null && ad.PictureFiles.Count > 0)
                {
                    pictures.Add(ad.PictureFiles[0]);
                }

                list.Add(new AdProductsModel
                {
                    id = ad.ID,
                    name = ad.Name,
                    price = ad.Price,
                    salePrice = ad.Price,
                    discount = 50,
                    shortDetails = ad.Description,
                    description = ad.Description,
                    category = ad.Category?.Name,
                    tags = new List<string> { ad.City?.Name },
                    //pictures = ad.PictureFiles,
                    pictures = pictures,
                    createdAt = ad.CreatedAt
                }) ;
            }

            return Ok(list);
        }

        [HttpGet, Route("getRelatedAds")]
        public IActionResult GetRelatedAds()
        {
            List<AdProductsModel> list = new List<AdProductsModel>();

            var adsArray = _adService.GetAdGridModel(new Guid("99DE8181-09A8-41DB-895E-54E5E0650C3A"));

            foreach (var ad in adsArray)
            {
                List<byte[]> pictures = new List<byte[]>();
                if (ad.PictureFiles != null && ad.PictureFiles.Count > 0)
                {
                    pictures.Add(ad.PictureFiles[0]);
                }

                list.Add(new AdProductsModel
                {
                    id = ad.ID,
                    name = ad.Name,
                    price = ad.Price,
                    salePrice = ad.Price,
                    discount = 50,
                    shortDetails = ad.Description,
                    description = ad.Description,
                    category = ad.Category?.Name,
                    tags = new List<string> { ad.City?.Name },
                    //pictures = ad.PictureFiles,
                    pictures = pictures,
                    createdAt = ad.CreatedAt
                });
            }

            return Ok(list);
        }

        [HttpPost("{id}"), Route("getProductDetails")]
        public IActionResult GetProductDetails([FromBody]AdGetModel model)
        {
            return Ok(_adService.GetAdAsViewModel(model.id));
        }
    }

}
