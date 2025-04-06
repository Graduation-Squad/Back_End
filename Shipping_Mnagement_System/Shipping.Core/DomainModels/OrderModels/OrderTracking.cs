using Shipping.Core.DomainModels.Identity;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels.OrderModels
{
    public class OrderTracking : BaseModel
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public OrderStatus Status { get; set; }
        public string Notes { get; set; }

        // Rejection details (if applicable)
        public int? RejectionReasonId { get; set; }
        public RejectionReason RejectionReason { get; set; }
        public string RejectionDetails { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
