using Shipping.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class PermissionByNameSpec : Specification<Permission>
    {
        public PermissionByNameSpec(string name) : base(p => p.Name == name) { }
    }
}
