using Shipping.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class ShippingType : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal AdditionalCost { get; set; } = 0;
        public int Days { get; set; } = 0;
        public bool IsActive { get; set; } = true;
    }

}
