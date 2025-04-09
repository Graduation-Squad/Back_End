using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class OrderSpecifications : Specification<Order>
    {
        public OrderSpecifications(OrderParameters orderParameters)
        {
            _addIncludes();

            OrderStatus? parsedStatus = null;
            if (!string.IsNullOrEmpty(orderParameters.Status) &&
                Enum.TryParse<OrderStatus>(orderParameters.Status, true, out var status))
            {
                parsedStatus = status;
            }

            AddCriteria(o =>
                (!orderParameters.MerchantId.HasValue || o.MerchantId == orderParameters.MerchantId) &&
                (!orderParameters.DeliveryAgentId.HasValue || o.DeliveryAgentId == orderParameters.DeliveryAgentId) &&
                (!orderParameters.BranchId.HasValue || o.BranchId == orderParameters.BranchId) &&
                (!orderParameters.AreaId.HasValue || o.AreaId == orderParameters.AreaId) &&
                (!orderParameters.CityId.HasValue || o.CityId == orderParameters.CityId) &&
                (!orderParameters.GovernorateId.HasValue || o.GovernorateId == orderParameters.GovernorateId) &&
                (!orderParameters.PaymentMethodId.HasValue || o.PaymentMethodId == orderParameters.PaymentMethodId) &&
                (!orderParameters.ShippingTypeId.HasValue || o.ShippingTypeId == orderParameters.ShippingTypeId) &&
                (!parsedStatus.HasValue || o.Status == parsedStatus)
            );


            if (!string.IsNullOrEmpty(orderParameters.SortBy))
            {
                AddSorting(orderParameters.SortBy, orderParameters.IsSortAscending);
            }
            if (orderParameters.PageNumber > 0 && orderParameters.PageSize > 0)
            {
                AddPaging(orderParameters.PageNumber, orderParameters.PageSize);
            }
        }
        public OrderSpecifications(int orderId)
        {
            AddCriteria(o => o.Id == orderId);
            _addIncludes();
        }
        private void _addIncludes()
        {
            AddInclude(o => o.Merchant);
            AddInclude(o => o.DeliveryAgent);
            AddInclude(o => o.Branch);
            AddInclude(o => o.Area);
            AddInclude(o => o.City);
            AddInclude(o => o.Governorate);
            AddInclude(o => o.PaymentMethod);
            AddInclude(o => o.ShippingType);
            AddInclude(o => o.OrderTrackings);
            AddInclude(o => o.CreatedBy);
        }
    }

    //public class OrderSpecifications : Specification<Order>
    //{
    //    public OrderSpecifications(OrderParameters orderParameters)
    //    {
    //        _addIncludes();

    //        if (!string.IsNullOrEmpty(orderParameters.Search))
    //        {
    //            AddCriteria(o =>
    //                o.Merchant.AppUser.Email.Contains(orderParameters.Search) ||
    //                o.DeliveryAgent.AppUser.Email.Contains(orderParameters.Search) ||
    //                o.Status.ToString().Contains(orderParameters.Search) ||
    //                o.Branch.Name.Contains(orderParameters.Search) ||
    //                o.Area.Name.Contains(orderParameters.Search) ||
    //                o.City.Name.Contains(orderParameters.Search) ||
    //                o.Governorate.Name.Contains(orderParameters.Search)
    //            );
    //        }

    //        if (!string.IsNullOrEmpty(orderParameters.Status) &&
    //            Enum.TryParse(orderParameters.Status, out OrderStatus parsedStatus))
    //        {
    //            AddCriteria(o => o.Status == parsedStatus);
    //        }

    //        if (orderParameters.StartDate.HasValue)
    //        {
    //            AddCriteria(o => o.CreatedAt >= orderParameters.StartDate.Value);
    //        }

    //        if (orderParameters.EndDate.HasValue)
    //        {
    //            AddCriteria(o => o.CreatedAt <= orderParameters.EndDate.Value);
    //        }

    //        if (!string.IsNullOrEmpty(orderParameters.SortBy))
    //        {
    //            AddSorting(orderParameters.SortBy, orderParameters.IsSortAscending);
    //        }

    //        if (orderParameters.PageNumber > 0 && orderParameters.PageSize > 0)
    //        {
    //            AddPaging(orderParameters.PageNumber, orderParameters.PageSize);
    //        }
    //    }

    //    public OrderSpecifications(int orderId)
    //    {
    //        AddCriteria(o => o.Id == orderId);
    //        _addIncludes();
    //    }

    //    private void _addIncludes()
    //    {
    //        AddInclude(o => o.Merchant);
    //        AddInclude(o => o.DeliveryAgent);
    //        AddInclude(o => o.Branch);
    //        AddInclude(o => o.Area);
    //        AddInclude(o => o.City);
    //        AddInclude(o => o.Governorate);
    //        AddInclude(o => o.PaymentMethod);
    //        AddInclude(o => o.ShippingType);
    //        AddInclude(o => o.OrderTrackings);
    //        AddInclude(o => o.CreatedBy);
    //    }
    //}

}
