using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class AdminDashboardDto
    {
        public int TotalMerchants { get; set; }
        public int TotalDeliveryAgents { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<OrderStatusCountDto> StatusCounts { get; set; } = new();
        public List<RevenueByBranchDto> RevenueByBranch { get; set; } = new();
    }
}
