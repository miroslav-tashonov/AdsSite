using Microsoft.AspNetCore.Mvc.Rendering;
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

        private List<int> PageSizesList { get { return new List<int> { 5, 10, 20 }; } }

        public int PageIndex { get; private set; }

        public List<SelectListItem> GetPageSizes(string selectedPageSize)
        {
            return PageSizesList.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ToString(),
                    Value = a.ToString(),
                    Selected = selectedPageSize == a.ToString()
                };
            });
        }


        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, int maximumPrice)
        {
            MaximumPrice = maximumPrice;
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

        public int MaximumPrice { get; set; }

        public static PaginatedList<T> CreatePageAsync(List<T> items, int count, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static PaginatedList<T> CreatePageAsync( List<T> items, int count, int pageIndex, int pageSize, int maximumPrice)
        {
            return new PaginatedList<T>(items, count, pageIndex, pageSize, maximumPrice);
        }
    }
}
