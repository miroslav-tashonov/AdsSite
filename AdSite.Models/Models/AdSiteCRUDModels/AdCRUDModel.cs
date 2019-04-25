﻿using AdSite.Models.CRUDModels.AuditedModels;
using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdSite.Models.CRUDModels
{
    public class AdViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


        public Category Category { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public AdDetail AdDetail { get; set; }
        public ApplicationUser Owner { get; set; }

        DateTime CreatedAt { get; set; }
        DateTime ModifiedAt { get; set; }
        ApplicationUser ModifiedBy { get; set; }
        ApplicationUser CreatedBy { get; set; }
    }


    public class AdGridViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description{ get; set; }
        public byte[] MainPicture { get; set; }


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
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public Guid CityId { get; set; }
        public Guid CountryId { get; set; }
        public AdDetail AdDetail { get; set; }
        public string OwnerId { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public List<byte[]> FilesAsListOfByteArray { get; set; }

    }
    public class AdEditModel : AuditedEntityModel
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public Guid CityId { get; set; }
        public AdDetail AdDetail { get; set; }
        public string OwnerId { get; set; }
    }

}