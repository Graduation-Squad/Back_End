using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels.OrderModels
{
    public class RejectionReason : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool RequiresDetails { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public RejectionReasonType Type { get; set; }
        public ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
    }
}
