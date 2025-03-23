using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }

        public ICollection<Area> Areas { get; set; } = new List<Area>();
    }
}
