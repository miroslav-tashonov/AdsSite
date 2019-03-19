using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.ViewModels
{
    public class CategoryViewModel : AuditedEntityViewModel
    {
        public Guid ID { get; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Children { get; set; }
    }

    public class CategoryCreateModel
    {
        public string Name { get; set; }
        public Guid ParentId { get; set; }
        public string Type { get; set; }
    }

    public class CategoryEditModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid ParentId { get; set; }
    }
}
