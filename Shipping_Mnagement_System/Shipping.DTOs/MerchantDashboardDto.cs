using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class MerchantDashboardDto
    {
        public int TotalOrders { get; set; } = 0;
        public int createdOrders { get; set; } = 0;
        public int assignedOrders { get; set; } = 0;
        public int processingOrders { get; set; } = 0;
        public int shippedOrders { get; set; } = 0;
        public int deliveredOrders { get; set; } = 0;
        public int cancelledOrders { get; set; } = 0;
        public int returnedOrders { get; set; } = 0;
        public int rejectedOrders { get; set; } = 0;        

        public decimal TotalRevenue { get; set; } = 0;
        public List<OrderStatusCountDto> StatusCounts { get; set; } = new();
        public List<WeeklyOrderStatsDto> WeeklyStats { get; set; } = new();
    }
}
