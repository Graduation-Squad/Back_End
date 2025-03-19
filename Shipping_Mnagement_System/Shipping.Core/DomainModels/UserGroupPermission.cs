using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class UserGroupPermission
    {
        public int UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
