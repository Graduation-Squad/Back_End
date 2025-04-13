using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.Enums;
using Shipping.Core.Permissions;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Core.Specification;
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
            ShippingContext context,
            IUnitOfWork unitOfWork)
        {
            // 1. Seed roles
            string[] roles = { "Admin", "Employee", "Merchant", "DeliveryMan" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2. Seed permissions
            await SeedPermissionsAsync(unitOfWork);

            // 3. Create default user groups with their permissions
            await EnsureDefaultUserGroupsExist(unitOfWork);

            // 4. Create initial admin user
            var adminGroup = await unitOfWork.Repository<UserGroup>()
                .GetWithSpecAsync(new UserGroupByNameSpec("Administrators"));

            await CreateUserIfNotExists(
                userManager, emailService, context, unitOfWork,
                "em468001@gmail.com", "Admin@123", "System Admin",
                UserType.Admin, "Admin", "Admin Address", "01000000000", adminGroup?.Id);
        }

        private static async Task SeedPermissionsAsync(IUnitOfWork unitOfWork)
        {
            var permissionsToAdd = new[]
            {
                // User Groups Permissions
                new Permission() { Name = Permissions.UserGroups.View, Description = "View user groups", Module = "UserGroups" },
                new Permission() { Name = Permissions.UserGroups.Create, Description = "Create user groups", Module = "UserGroups" },
                new Permission() { Name = Permissions.UserGroups.Edit, Description = "Edit user groups", Module = "UserGroups" },
                new Permission() { Name = Permissions.UserGroups.Delete, Description = "Delete user groups", Module = "UserGroups" },
                new Permission() { Name = Permissions.UserGroups.ManagePermissions, Description = "Manage user group permissions", Module = "UserGroups" },

                // Users Permissions
                new Permission() { Name = Permissions.Users.View, Description = "View users", Module = "Users" },
                new Permission() { Name = Permissions.Users.Create, Description = "Create users", Module = "Users" },
                new Permission() { Name = Permissions.Users.Edit, Description = "Edit users", Module = "Users" },
                new Permission() { Name = Permissions.Users.Delete, Description = "Delete users", Module = "Users" },
                new Permission() { Name = Permissions.Users.ManageStatus, Description = "Manage user status", Module = "Users" },

                // Orders Permissions
                new Permission() { Name = Permissions.Orders.View, Description = "View orders", Module = "Orders" },
                new Permission() { Name = Permissions.Orders.Create, Description = "Create orders", Module = "Orders" },
                new Permission() { Name = Permissions.Orders.Edit, Description = "Edit orders", Module = "Orders" },
                new Permission() { Name = Permissions.Orders.UpdateStatus, Description = "Update order status", Module = "Orders" },
                new Permission() { Name = Permissions.Orders.Assign, Description = "Assign orders", Module = "Orders" },

                // Locations Permissions
                new Permission() { Name = Permissions.Locations.ViewGovernorates, Description = "View governorates", Module = "Locations" },
                new Permission() { Name = Permissions.Locations.ManageGovernorates, Description = "Manage governorates", Module = "Locations" },
                new Permission() { Name = Permissions.Locations.ViewCities, Description = "View cities", Module = "Locations" },
                new Permission() { Name = Permissions.Locations.ManageCities, Description = "Manage cities", Module = "Locations" },
                new Permission() { Name = Permissions.Locations.ViewAreas, Description = "View areas", Module = "Locations" },
                new Permission() { Name = Permissions.Locations.ManageAreas, Description = "Manage areas", Module = "Locations" },
                
                // Dashboard Permissions
                new Permission() { Name = Permissions.Dashboard.ViewMerchant, Description = "View merchant dashboard", Module = "Dashboard" },
                new Permission() { Name = Permissions.Dashboard.ViewEmployee, Description = "View employee dashboard", Module = "Dashboard" },
                new Permission() { Name = Permissions.Dashboard.ViewAdmin, Description = "View admin dashboard", Module = "Dashboard" }
            };

            foreach (var permission in permissionsToAdd)
            {
                var existing = await unitOfWork.Repository<Permission>()
                    .GetWithSpecAsync(new PermissionByNameSpec(permission.Name));

                if (existing == null)
                {
                    await unitOfWork.Repository<Permission>().AddAsync(permission);
                }
            }
            await unitOfWork.CompleteAsync();
        }

        private static async Task EnsureDefaultUserGroupsExist(IUnitOfWork unitOfWork)
        {
            // Get all permissions once
            var allPermissions = await unitOfWork.Repository<Permission>().GetAllAsync();

            // Administrators group (all permissions)
            await EnsureUserGroupExists(unitOfWork, "Administrators", allPermissions);

            // Merchants group
            var merchantPermissions = allPermissions.Where(p =>
                p.Name == Permissions.Dashboard.ViewMerchant ||
                p.Name == Permissions.Orders.Create ||
                p.Name == Permissions.Orders.View).ToList();
            await EnsureUserGroupExists(unitOfWork, "Merchants", merchantPermissions);

            // Employees group
            var employeePermissions = allPermissions.Where(p =>
                p.Name == Permissions.Dashboard.ViewEmployee ||
                p.Name == Permissions.Orders.View ||
                p.Name == Permissions.Orders.UpdateStatus ||
                p.Name == Permissions.Orders.Assign).ToList();
            await EnsureUserGroupExists(unitOfWork, "Employees", employeePermissions);

            // Delivery Personnel group
            var deliveryPermissions = allPermissions.Where(p =>
                p.Name == Permissions.Orders.View ||
                p.Name == Permissions.Orders.UpdateStatus).ToList();
            await EnsureUserGroupExists(unitOfWork, "Delivery Personnel", deliveryPermissions);
        }

        private static async Task EnsureUserGroupExists(
            IUnitOfWork unitOfWork,
            string name,
            System.Collections.Generic.IEnumerable<Permission> permissions)
        {
            var existingGroup = await unitOfWork.Repository<UserGroup>()
                .GetWithSpecAsync(new UserGroupByNameSpec(name));

            if (existingGroup == null)
            {
                var newGroup = new UserGroup
                {
                    Name = name,
                    UserGroupPermissions = permissions.Select(p => new UserGroupPermission
                    {
                        PermissionId = p.Id
                    }).ToList()
                };

                await unitOfWork.Repository<UserGroup>().AddAsync(newGroup);
                await unitOfWork.CompleteAsync();
            }
        }

        private static async Task CreateUserIfNotExists(
            UserManager<AppUser> userManager,
            IEmailService emailService,
            ShippingContext context,
            IUnitOfWork unitOfWork,
            string email,
            string password,
            string fullName,
            UserType userType,
            string role,
            string address,
            string phoneNumber,
            int? userGroupId = null)
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
                GovernorateId = defaultGovernorate.Id,
                UserGroupId = userGroupId
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                Console.WriteLine($"✅ {role} user created: {email}");

                await emailService.SendEmailAsync(
                    email,
                    "Welcome to Shipping System",
                    $"Hello {fullName},\n\nYour login credentials are:\nUsername: {email}\nPassword: {password}\n\nThank you!");
            }
            else
            {
                Console.WriteLine($"❌ Failed to create {role}: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}