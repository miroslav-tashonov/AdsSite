using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models
{
    public class FilterModel
    {
        public int MinimumPrice{ get; set; }
        public int MaximumPrice { get; set; }
        public Guid? CategoryId { get; set; }
        public List<Guid> CityIds { get; set; }
    }

    public class FilterRepositoryModel
    {
        public int MinimumPrice { get; set; }
        public int MaximumPrice { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public List<Guid> CityIds { get; set; }

        public Guid CountryId { get; set; }
    }

    
}
