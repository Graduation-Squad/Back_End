using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class WeightSetting : BaseModel
    {
        public decimal BaseWeight { get; set; }
        public decimal AdditionalWeightPrice { get; set; }
        public decimal BaseWeightPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
    }
}
