using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.AdSiteDomainModels
{
    public class AdDetailPicture
    {
        public int AdDetailPictureID { get; set; } // primary key
        //todo: image file property

        #region Foreign Keys
        public int AdDetailID { get; set; }
        public AdDetail AdDetail { get; set; }
        #endregion

    }
}
