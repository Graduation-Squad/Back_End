using Shipping.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class Area : BaseModel 
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public int CityId { get; set; }
        public City? City { get; set; }

    }
}
