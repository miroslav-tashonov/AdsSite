using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public const int NUMBER_OF_PAGES_PER_SIDE = 5;

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
                    (TotalPages - PageIndex > NUMBER_OF_PAGES_PER_SIDE) ?
                    NUMBER_OF_PAGES_PER_SIDE + ((PageIndex - 1 > NUMBER_OF_PAGES_PER_SIDE) ? 0 : PageIndex - 1) : 
                    TotalPages - PageIndex ;
            }
        }

        public int PagesBefore
        {
            get
            {
                return (PageIndex - 1 > NUMBER_OF_PAGES_PER_SIDE) ?
                    NUMBER_OF_PAGES_PER_SIDE + ((TotalPages - PageIndex > NUMBER_OF_PAGES_PER_SIDE) ? 0 : TotalPages-PageIndex) : 
                    PageIndex-1; 
            }
        }

        public static PaginatedList<T> CreatePageAsync( List<T> items, int count, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
