using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.AdSiteDomainModels
{
    public class Category
    {
        public int CategoryID { get; set; }
        

        public string Name { get; set; }

        /*todo: circular category entity
        public int ParentCategoryID { get; set; }
        public Category ParentCategory { get; set; }
        */

        #region Foreign Keys & navigation properties
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Ad> Ads { get; set; }
        #endregion
    }
}
