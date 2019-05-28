using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models
{
    public class PageQueryModel
    {
        public string ColumnName { get; set; }
        public string SearchString { get; set; }
        public string SortColumn { get; set; }
        public int? PageNumber { get; set; }


        public Guid? CategoryId { get; set; }
        public List<Guid> CityIds { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
