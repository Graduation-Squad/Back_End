using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class DeliveryManDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string VehicleNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
    }
}
