using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public int PagesLeft
        {
            get
            {
                return 
                    (TotalPages - PageIndex > 5) ? 
                    5 + ((PageIndex - 1 > 5) ? 0 : PageIndex - 1) : 
                    TotalPages - PageIndex ;
            }
        }

        public int PagesBefore
        {
            get
            {
                return (PageIndex - 1 > 5 ) ? 
                    5 + ((TotalPages - PageIndex > 5) ? 0 : TotalPages-PageIndex) : 
                    PageIndex-1; 
            }
        }

        public static PaginatedList<T> CreatePageAsync( List<T> items, int count, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
