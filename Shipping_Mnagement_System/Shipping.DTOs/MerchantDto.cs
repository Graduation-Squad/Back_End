using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class MerchantDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StoreName { get; set; }
        public decimal PickupCost { get; set; }
        public decimal RejectedOrdersShippingRatio { get; set; }
    }
}
