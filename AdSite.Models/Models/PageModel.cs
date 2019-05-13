using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdSite.Models
{
    public class PageModel
    {
        public string ColumnName { get; set; }
        public string SearchString { get; set; }
        public string SortColumn { get; set; }
        public Guid CountryId { get; set; }
        public string CurrentUser { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
