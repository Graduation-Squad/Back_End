using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class WeightSettingDto
    {
        public int Id { get; set; }
        public decimal BaseWeight { get; set; }
        public decimal BaseWeightPrice { get; set; }
        public decimal AdditionalWeightPrice { get; set; }
        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; }
    }
}
