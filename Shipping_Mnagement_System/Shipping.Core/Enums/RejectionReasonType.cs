using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Enums
{
    public enum RejectionReasonType
    {
        DeliveryIssue = 1,  // Issues related to delivery
        ProductIssue = 2,   // Issues related to the product
        CustomerRequest = 3 // Customer-initiated rejection
    }
}
