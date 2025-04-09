using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class CreateOrderTrackingDto
    {
        public string Status { get; set; }
        public string Notes { get; set; }
        public int? RejectionReasonId { get; set; }
        public string RejectionDetails { get; set; }
    }

}
