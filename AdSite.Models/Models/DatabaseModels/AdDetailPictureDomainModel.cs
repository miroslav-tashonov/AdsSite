using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Models.DatabaseModels
{
    public class AdDetailPicture : StampBaseClass
    {
        #region Foreign Keys
        public Guid AdDetailID { get; set; }
        public AdDetail AdDetail { get; set; }
        #endregion

        public byte[] File { get; set; }
        public string Name { get; set; }
        public bool IsMainPicture { get; set; }
        public string Type { get; set; }
    }
}
