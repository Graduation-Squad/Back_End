using Microsoft.AspNetCore.Identity;
using Shipping.Core.DomainModels;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public UserType UserType { get; set; }

        // Relationships
        public int? UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
    }
}
