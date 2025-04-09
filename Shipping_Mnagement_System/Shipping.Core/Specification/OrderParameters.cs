using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class OrderParameters
    {
        private const int MAX_PAGE_SIZE = 10;
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public bool IsSortAscending { get; set; } = true;
        public int PageNumber { get; set; } = 1;
        private int pageSize { get; set; } = 10;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }
        //public OrderParameters(string? search, string? sortBy, bool isSortAscending, int pageNumber, int pageSize)
        //{
        //    Search = search;
        //    SortBy = sortBy;
        //    IsSortAscending = isSortAscending;
        //    PageNumber = pageNumber;
        //    PageSize = pageSize;
        //}
    }
}
