using Shipping.Core.Models;
using Shipping.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class UserGroup : BaseModel
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Relationships
        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public ICollection<UserGroupPermission> UserGroupPermissions { get; set; } = new List<UserGroupPermission>();
    }
}
