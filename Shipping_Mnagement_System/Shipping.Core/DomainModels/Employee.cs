using Shipping.Core.DomainModels.Identity;
using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class Employee : BaseModel
    {
        public string EmployeeCode { get; set; }
        public string Department { get; set; }
        public ICollection<Order> CreatedOrders { get; set; } = new List<Order>();
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
