using Shipping.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string Department { get; set; }

        // FK to AppUser
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
