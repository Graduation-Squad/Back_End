using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class OrderCreateDto
    {
        [Required]
        public int MerchantId { get; set; }

        [Required]
        public int AreaId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int GovernorateId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal TotalWeight { get; set; }

        [Required]
        public int  PaymentMethodId { get; set; }

        [Required]
        public int DeliveryOptionId { get; set; }

        [Required]
        public int BranchId { get; set; }

        public decimal CODAmount { get; set; }
        public string Notes { get; set; }
    }
}
