using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class AdDetailPicture : StampBaseClass
    {
        //todo: image file property

        #region Foreign Keys
        public Guid AdDetailID { get; set; }
        public AdDetail AdDetail { get; set; }
        #endregion

    }
}
