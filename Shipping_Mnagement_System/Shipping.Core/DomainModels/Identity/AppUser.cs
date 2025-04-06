using Microsoft.AspNetCore.Identity;
using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public UserType UserType { get; set; }
        public int? UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
        public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
        public ICollection<Order> CreatedOrders { get; set; } = new List<Order>(); // Orders created by this user (merchant/employee)
        public ICollection<Order> AssignedOrders { get; set; } = new List<Order>(); // Orders assigned to this user (delivery agent)
        public ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();


    }
}
