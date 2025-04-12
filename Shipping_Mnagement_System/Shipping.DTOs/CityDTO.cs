﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class CityDTO
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public int GovernorateId { get; set; }
    }
}
