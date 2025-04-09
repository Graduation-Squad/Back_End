using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    //public class OrderParameters
    //{
    //    private const int MAX_PAGE_SIZE = 10;
    //    public int? MerchantId { get; set; }
    //    public int? DeliveryAgentId { get; set; }
    //    public int? BranchId { get; set; }
    //    public int? AreaId { get; set; }
    //    public int? CityId { get; set; }
    //    public int? GovernorateId { get; set; }
    //    public string? Status { get; set; }
    //    public int? PaymentMethodId { get; set; }
    //    public int? ShippingTypeId { get; set; }
    //    public string? SortBy { get; set; }
    //    public bool IsSortAscending { get; set; } = true;
    //    public int PageNumber { get; set; } = 1;
    //    private int pageSize { get; set; } = 10;
    //    public int PageSize
    //    {
    //        get => pageSize;
    //        set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
    //    }
    //    //public OrderParameters(string? search, string? sortBy, bool isSortAscending, int pageNumber, int pageSize)
    //    //{
    //    //    Search = search;
    //    //    SortBy = sortBy;
    //    //    IsSortAscending = isSortAscending;
    //    //    PageNumber = pageNumber;
    //    //    PageSize = pageSize;
    //    //}
    //}

     

    
        public class OrderParameters
        {
            private const int MAX_PAGE_SIZE = 10;

            // 🔍 Filtering
            public int? MerchantId { get; set; }
            public int? DeliveryAgentId { get; set; }
            public int? BranchId { get; set; }
            public int? AreaId { get; set; }
            public int? CityId { get; set; }
            public int? GovernorateId { get; set; }
            public string? Status { get; set; }
            public int? PaymentMethodId { get; set; }
            public int? ShippingTypeId { get; set; }

            // 🔁 Sorting & Pagination
            public string? SortBy { get; set; }
            public bool IsSortAscending { get; set; } = true;
            public int PageNumber { get; set; } = 1;
            private int pageSize = 10;
            public int PageSize
            {
                get => pageSize;
                set => pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
            }

            // 🆕 Additional Filters
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string? Search { get; set; }
        }
    }


