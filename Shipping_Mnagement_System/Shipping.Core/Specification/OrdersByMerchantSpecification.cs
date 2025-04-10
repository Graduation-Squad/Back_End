using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class OrdersByMerchantSpecification : Specification<Order>
    {
        public OrdersByMerchantSpecification(int merchantId)
            : base(o => o.MerchantId == merchantId)
        {
            AddInclude(o => o.DeliveryAgent);
            AddInclude(o => o.Branch);

        }

        //public OrdersByMerchantSpecification WithCreatedDateAfter(DateTime date)
        //{
        //    AddCriteria(o => o.CreatedAt >= date);
        //    return this;
        //}

        //public OrdersByMerchantSpecification WithStatus(OrderStatus status)
        //{
        //    AddCriteria(o => o.Status == status);
        //    return this;
        //}
    }
}
