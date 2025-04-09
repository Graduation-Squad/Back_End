using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class OrderUpdateDto
    {
        public int? AreaId { get; set; }
        public int? CityId { get; set; }
        public int? GovernorateId { get; set; }
        public decimal? TotalWeight { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? DeliveryOptionId { get; set; }
        public decimal? CODAmount { get; set; }
        public string Notes { get; set; }
    }
}
