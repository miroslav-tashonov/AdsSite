using AdSite.Models.CRUDModels.AuditedModels;
using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdSite.Models.CRUDModels
{
    public class WishlistAdGridModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public byte[] MainPicture { get; set; }
    }


    public class AdViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsInWishlist { get; set; }

        public List<AdGridViewModel> RelatedAds { get; set; }
        public Category Category { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public AdDetail AdDetail { get; set; }
        public ApplicationUser Owner { get; set; }

        public DateTime CreatedAt { get; set; }
    }


    public class AdGridViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] MainPicture { get; set; }
        public bool IsInWishlist {get; set;}

        public DateTime CreatedAt { get; set; }
        public List<byte[]> PictureFiles { get; set; }

        public Category Category { get; set; }
        public City City { get; set; }        
        public ApplicationUser Owner { get; set; }
    }

    public class AdGridPagedModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] MainPicture { get; set; }


        public Category Category { get; set; }
        public City City { get; set; }
        public ApplicationUser Owner { get; set; }
    }


    public class AdCreateModel : AuditedEntityModel
    {
        public AdCreateModel()
        {
            FilesAsListOfByteArray = new List<byte[]>();
        }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public Guid CityId { get; set; }
        public AdDetail AdDetail { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public string MainPictureFile { get; set; }
        public List<byte[]> FilesAsListOfByteArray { get; set; }
        public string SerializedAdDetailsPictures { get; set; }
        public byte[] MainPictureThumbnail { get; set; }

    }
    public class AdEditModel : AuditedEntityModel
    {
        public AdEditModel()
        {
            FilesAsListOfByteArray = new List<byte[]>();
            AdDetail = new AdDetail();
        }

        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        public string OwnerId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CityId { get; set; }
        public AdDetail AdDetail { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }
        public string MainPictureFile{ get; set; }
        public List<byte[]> FilesAsListOfByteArray { get; set; }
        public byte[] MainPictureThumbnail { get; set; }

        public string SerializedAdDetailsPictures { get; set; }
        public string SerializedMainPicture { get; set; }
    }

    public class AdGetModel
    {
        public Guid id { get; set; }
    }

    public class AdGetWithCountryModel
    {
        public Guid countryId { get; set; }
    }


    public class AdProductsModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal salePrice { get; set; }
        public int discount { get; set; }

        public DateTime createdAt { get; set; }

        public List<byte[]> pictures = new List<byte[]>();
        public string shortDetails { get; set; }
        public string description { get; set; }

        public string category { get; set; }

        public List<string> tags = new List<string>();

        public bool sale = false;

        public bool @new = false;

        public bool advertised = true;

        public List<string> colors = new List<string>() { "yellow", "gray", "green" };

        public List<VariantClass> variants = new List<VariantClass>()
        {
            new VariantClass
            {
                color = "yellow",
            },
            new VariantClass
            {
                color = "gray",
            },
            new VariantClass
            {
                color = "green",
            },
        };
    }

    public class VariantClass
    {
        public string color { get; set; }
        public string images { get; set; }
    }

    public enum ColorsClass
    {
        Yellow = 1,
        Gray = 2,
        Green = 3,
        Red = 4,
        Orange = 5,
        Black = 6,
        White = 7,
        Blue = 8,
        Cyan = 9,
        Magenta = 10,
    }

}
