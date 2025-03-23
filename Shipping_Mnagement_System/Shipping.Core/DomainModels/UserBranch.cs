using Shipping.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.DomainModels
{
    public class UserBranch
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}
