using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Models.Identity
{
    public class AppUser : IdentityUser
    {
        // Custom properties
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        //navigation properties
        public Employee Employee { get; set; }
        public Merchant Merchant { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
    }
}
