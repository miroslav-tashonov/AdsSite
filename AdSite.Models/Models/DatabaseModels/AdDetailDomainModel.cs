﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class AdDetail : StampBaseClass
    {
        public string Description { get; set; }
        public byte[] MainPictureThumbnailFile { get; set; }

        #region Foreign Keys
        public Guid AdID { get; set; }
        public Ad Ad { get; set; }
        public ICollection<AdDetailPicture> AdDetailPictures { get; set; }
        #endregion

    }
}
