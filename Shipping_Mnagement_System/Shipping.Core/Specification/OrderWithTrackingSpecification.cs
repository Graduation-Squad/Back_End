using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class OrderWithTrackingSpecification : Specification<Order>
    {
        public OrderWithTrackingSpecification(int orderId)
            : base(o => o.Id == orderId)
        {
            AddInclude(o => o.OrderTrackings);
            AddInclude(o => o.OrderTrackings.Select(t => t.User));
            AddInclude(o => o.OrderTrackings.Select(t => t.RejectionReason));
        }
    }


}
