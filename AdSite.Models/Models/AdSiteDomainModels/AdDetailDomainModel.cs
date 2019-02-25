using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.AdSiteDomainModels
{
    public class AdDetail
    {
        public int AdDetailID { get; set; } // primary key

        public string Description { get; set; }
        

        #region Foreign Keys
        public int AdID { get; set; }
        public Ad Ad { get; set; }
        public ICollection<AdDetailPicture> AdDetailPictures { get; set; }
        #endregion

    }
}
