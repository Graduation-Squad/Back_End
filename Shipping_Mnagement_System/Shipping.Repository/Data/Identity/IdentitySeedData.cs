using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.Enums;
using Shipping.Core.Services.Contracts;
using Shipping.Repository.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.Repository.Data.Identity
{
    public static class IdentitySeedData
    {
        public static async Task Initialize(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            IEmailService emailService,
            ShippingContext context)
        {
            
            string[] roles = { "Admin", "Employee", "Merchant", "DeliveryMan" };

            
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            
            await CreateUserIfNotExists(userManager, emailService, context, "em468001@gmail.com", "Admin@123", "System Admin", UserType.Admin, "Admin", "Admin Address", "01000000000");
          //  await CreateUserIfNotExists(userManager, emailService, context, "merchant@shipping.com", "Merchant@123", "Default Merchant", UserType.Merchant, "Merchant", "Merchant Address", "01000000001");
          //  await CreateUserIfNotExists(userManager, emailService, context, "delivery@shipping.com", "Delivery@123", "Default Delivery", UserType.DeliveryAgent, "DeliveryMan", "Delivery Address", "01000000002");
        }

       
        private static async Task CreateUserIfNotExists(
            UserManager<AppUser> userManager,
            IEmailService emailService,
            ShippingContext context,
            string email,
            string password,
            string fullName,
            UserType userType,
            string role,
            string address,
            string phoneNumber)
        {
           
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null) return;

           
            var defaultArea = await context.Areas.FirstOrDefaultAsync(a => a.Id == 1);
            var defaultCity = await context.Cities.FirstOrDefaultAsync(c => c.Id == 1);
            var defaultGovernorate = await context.Governorates.FirstOrDefaultAsync(g => g.Id == 1);

            
            if (defaultArea == null || defaultCity == null || defaultGovernorate == null)
            {
                throw new InvalidOperationException("Default Area, City, or Governorate not found.");
            }

            
            var user = new AppUser
            {
                Email = email,
                UserName = email.Split('@')[0],
                FullName = fullName,
                EmailConfirmed = true,
                PhoneNumber = phoneNumber,
                UserType = userType,
                Address = address,
                AreaId = defaultArea.Id,  
                CityId = defaultCity.Id,  
                GovernorateId = defaultGovernorate.Id  
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                Console.WriteLine($"✅ {role} user created: {email}");

             
                await emailService.SendEmailAsync(email, "Welcome to Shipping System", $"Hello {fullName},\n\nYour login credentials are:\nUsername: {email}\nPassword: {password}\n\nThank you!");
            }
            else
            {
                Console.WriteLine($"❌ Failed to create {role}: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
