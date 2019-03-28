﻿using AdSite.Models.CRUDModels.AuditedModels;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class CategoryViewModel : AuditedEntityViewModel
    {
        public Guid ID { get; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }

    public class CategoryCreateModel : AuditedEntityModel
    {
        [Required]
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        [Required]
        public string Type { get; set; }
    }

    public class CategoryEditModel : AuditedEntityModel
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public Guid ParentId { get; set; }
    }
}
