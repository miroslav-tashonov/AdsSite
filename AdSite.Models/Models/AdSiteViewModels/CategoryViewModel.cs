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
        public Guid ParentCategoryID { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> Children { get; set; }

    }
}
