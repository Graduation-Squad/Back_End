using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class OrderStatusUpdateDto
    {
        [Required]
        public string Status { get; set; }
        public int? RejectionReasonId { get; set; }
        public string RejectionDetails { get; set; }
    }
}
