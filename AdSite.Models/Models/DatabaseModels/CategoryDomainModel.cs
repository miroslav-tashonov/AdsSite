using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class Category : StampBaseClass
    {
        public Category()
        {
            Children = new List<Category>();
        }
        [Required]
        public string Name { get; set; }

        #region Foreign Keys & navigation properties
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Ad> Ads { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; }
        #endregion
    }
}
