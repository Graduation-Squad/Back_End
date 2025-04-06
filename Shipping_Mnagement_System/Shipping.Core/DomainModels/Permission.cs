using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class Permission : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }

        // Relationships
        public ICollection<UserGroupPermission> UserGroupPermissions { get; set; } = new List<UserGroupPermission>();
    }
}
