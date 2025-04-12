using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Specification
{
    public class UserWithUserGroupAndPermissionsSpec : Specification<AppUser>
    {
        public UserWithUserGroupAndPermissionsSpec(string userId) : base(x => x.Id == userId)
        {
            //AddInclude("UserGroup.UserGroupPermissions.Permission");
        }
    }
}
