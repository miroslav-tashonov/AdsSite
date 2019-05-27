using AdSite.Models.DatabaseModels;
using AdSite.Models.Models.AdSiteViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models.CRUDModels
{
    public class CitiesFilterViewModel
    {
        public CitiesFilterViewModel()
        {
            ComponentCities = new List<LookupViewModel>();
        }

        public bool IsFirst { get; set; }
        public ICollection<LookupViewModel> ComponentCities { get; set; }
    }

}
