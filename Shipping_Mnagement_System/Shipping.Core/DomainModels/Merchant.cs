using Shipping.Core.DomainModels.Identity;
using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class Merchant : BaseModel
    {
        public string StoreName { get; set; }
        public decimal PickupCost { get; set; }
        public decimal RejectedOrdersShippingRatio { get; set; }
        public ICollection<Order> CreatedOrders { get; set; } = new List<Order>();
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
