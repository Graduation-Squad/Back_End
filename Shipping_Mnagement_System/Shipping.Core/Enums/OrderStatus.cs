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
        Processing = 2,     
        Shipped = 3,        
        Delivered = 4,      
        Rejected = 5, 
        Returned = 6, 
        Cancelled = 7  
    }
}
