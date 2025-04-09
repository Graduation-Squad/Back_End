using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class MerchantSummaryDto
    {
        public string MerchantName { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
