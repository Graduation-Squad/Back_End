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
        public string Address { get; set; } // = area, city, governorate
        public int? AreaId { get; set; }
        public Area Area { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public int? GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        public UserType UserType { get; set; }
        public int? UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
        public ICollection<UserBranch> UserBranches { get; set; } = new List<UserBranch>();
        public ICollection<OrderTracking> OrderTrackings { get; set; } = new List<OrderTracking>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public Employee Employee { get; set; }
        public Merchant Merchant { get; set; }
        public DeliveryMan DeliveryMan { get; set; }
    }
}
