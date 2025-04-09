using Shipping.Core.DomainModels.Identity;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels.OrderModels
{
    public class Order : BaseModel
    {
        public string OrderNumber { get; set; }
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        // location
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }

        // order details
        public decimal TotalWeight { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal CODAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public string Notes { get; set; }

        // configurable options
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int ShippingTypeId { get; set; }
        public ShippingType ShippingType { get; set; }

        // tracking
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // relationships
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }

        public int? DeliveryAgentId { get; set; }
        public DeliveryMan DeliveryAgent { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
    }
}
