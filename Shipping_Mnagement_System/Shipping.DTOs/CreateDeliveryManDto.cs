using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class CreateDeliveryManDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }

}
