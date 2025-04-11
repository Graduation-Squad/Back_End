using Shipping.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class UserGroupByNameSpec : Specification<UserGroup>
    {
        public UserGroupByNameSpec(string name) : base(g => g.Name == name) { }
    }
}
