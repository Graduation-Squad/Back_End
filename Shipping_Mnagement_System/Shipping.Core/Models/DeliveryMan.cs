using Shipping.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Models
{
    public class DeliveryMan : BaseModel
    {
        public string VehicleNumber { get; set; }
        public string LicenseNumber { get; set; }

        // FK to AppUser
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
