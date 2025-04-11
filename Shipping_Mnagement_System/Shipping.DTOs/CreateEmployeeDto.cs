using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Models
{
    public class CreateEmployeeDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeCode { get; set; }
        public string Department { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }

}
