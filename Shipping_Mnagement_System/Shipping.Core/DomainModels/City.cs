using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class City : BaseModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public decimal DefaultShippingCost { get; set; } = 0;
        public int GovernorateId { get; set; }
        public Governorate? Governorate { get; set; }

        public ICollection<Area> Areas { get; set; } = new List<Area>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
