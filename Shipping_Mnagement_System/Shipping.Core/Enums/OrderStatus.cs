using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Enums
{
    public enum OrderStatus
    {
        Created = 1,
        Assigned = 2,     // 👈 Add this here
        Processing = 3,
        Shipped = 4,
        Delivered = 5,
        Rejected = 6,
        Returned = 7,
        Cancelled = 8
    }

}
