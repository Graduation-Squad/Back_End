using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class OrdersByEmployeeSpecification : Specification<Order>
    {
        public OrdersByEmployeeSpecification(int employeeId)
            : base(o => o.CreatedById == employeeId)
        {
            AddInclude(o => o.Merchant);
            AddInclude(o => o.Branch);
        }
    }
}
