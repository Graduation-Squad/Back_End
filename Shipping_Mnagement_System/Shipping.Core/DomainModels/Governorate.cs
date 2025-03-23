using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class Governorate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<City> Cities { get; set; } = new List<City>();
        public ICollection<WeightSetting> WeightSettings { get; set; } = new List<WeightSetting>();
    }
}
