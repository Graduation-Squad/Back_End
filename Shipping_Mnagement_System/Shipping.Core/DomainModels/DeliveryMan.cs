using Shipping.Core.DomainModels.Identity;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class DeliveryMan : BaseModel
    {
        public string VehicleNumber { get; set; }
        public string LicenseNumber { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public ICollection<Order> AssignedOrders { get; set; } = new List<Order>();
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
