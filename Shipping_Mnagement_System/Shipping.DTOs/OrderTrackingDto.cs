using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class OrderTrackingDto
    {
        public string Status { get; set; }
        public string Notes { get; set; }
        public string RejectionReason { get; set; }
        public string RejectionDetails { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
