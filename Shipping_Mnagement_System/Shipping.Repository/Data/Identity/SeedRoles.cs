using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository.Data.Identity
{
    public static class SeedRoles
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Employee", "Merchant", "DeliveryMan" };

            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
