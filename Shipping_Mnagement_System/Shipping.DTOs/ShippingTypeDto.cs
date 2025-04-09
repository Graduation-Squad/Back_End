﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class ShippingTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AdditionalCost { get; set; }
        public int Days { get; set; }

    }
}
