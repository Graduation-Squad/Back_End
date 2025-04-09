using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.Enums;

namespace Shipping.Repository.Data.Identity
{
    public static class IdentitySeedData
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            string[] roles = { "Admin", "Employee", "Merchant", "DeliveryMan" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            string adminEmail = "admin@shipping.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new AppUser
                {
                    Email = adminEmail,
                    UserName = "admin",
                    FullName = "System Admin",
                    EmailConfirmed = true,
                    PhoneNumber = "01000000000",
                    UserType = UserType.Admin
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    Console.WriteLine("✅ Default admin user created.");
                }
                else
                {
                    Console.WriteLine("❌ Error creating admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                Console.WriteLine("ℹ️ Admin user already exists.");
            }
        }
    }

}

