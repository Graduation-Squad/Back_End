using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class DeliveryPerformanceDto
    {
        public int TotalDelivered { get; set; }
        public int TotalRejected { get; set; }
        public double DeliveryRate { get; set; }
    }
}
