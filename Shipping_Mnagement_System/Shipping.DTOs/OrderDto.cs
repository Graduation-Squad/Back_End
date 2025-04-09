using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public  class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public MerchantDto Merchant { get; set; }
        public DeliveryManDto DeliveryMan { get; set; }
        public LocationDto DeliveryLocation { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal CODAmount { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public PaymentMethodDto PaymentMethod { get; set; }
        public ShippingTypeDto ShippingType { get; set; }
        public BranchDto Branch { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
