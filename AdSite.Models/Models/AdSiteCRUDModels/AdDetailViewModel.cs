using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class AdDetailViewModel
    {
        public string Description { get; set; }
        public Ad Ad { get; set; }
        public ICollection<AdDetailPictureViewModel> AdDetailPictures { get; set; }
    }
}
