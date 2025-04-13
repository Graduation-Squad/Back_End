using Microsoft.AspNetCore.Identity;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.DomainModels;
using Shipping.Core.Enums;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shipping.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Shipping.Service
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ShippingContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminService(UserManager<AppUser> userManager, IEmailService emailService, ShippingContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
            _httpContextAccessor = httpContextAccessor; // To access the current user
        }

        // Method to get the current logged-in user
        private async Task<AppUser> GetCurrentUserAsync()
        {
            // Get the currently logged-in user
            var userId = _httpContextAccessor.HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? await _userManager.FindByIdAsync(userId) : null;
        }

        // Method to check if the current logged-in user is an Admin
        private async Task<bool> IsAdminAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return false;

            // Check if the current user has the Admin role
            var roles = await _userManager.GetRolesAsync(user);
            return roles.Contains("Admin");  // Ensure this role matches exactly with your role setup
        }

        // Method to create a Merchant (Only accessible by Admin)
        public async Task<bool> CreateMerchantAsync(CreateMerchantDto dto)
        {
            // Check if the logged-in user is an Admin
            if (!await IsAdminAsync())
                throw new UnauthorizedAccessException("Only Admin can create new users.");

            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = true,
                UserType = UserType.Merchant,
                Address = dto.Address,
                UserGroupId = await _context.UserGroups
                    .Where(ug => ug.Name == "Merchants")
                    .Select(ug => ug.Id)
                    .FirstOrDefaultAsync()
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Merchant creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "Merchant");

            var merchant = new Merchant
            {
                AppUserId = user.Id,
                StoreName = dto.StoreName,
                PickupCost = dto.PickupCost,
                RejectedOrdersShippingRatio = dto.RejectedOrdersShippingRatio
            };

            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(
                user.Email,
                "Your Merchant Account",
                $"Username: {user.Email}\nPassword: {dto.Password}");

            return true;
        }

        // Method to create a DeliveryMan (Only accessible by Admin)
        public async Task<bool> CreateDeliveryManAsync(CreateDeliveryManDto dto)
        {
            // Check if the logged-in user is an Admin
            if (!await IsAdminAsync())
                throw new UnauthorizedAccessException("Only Admin can create new users.");

            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = true,
                UserType = UserType.DeliveryAgent,
                Address = dto.Address,
                UserGroupId = await _context.UserGroups
                    .Where(ug => ug.Name == "Delivery Personnel")
                    .Select(ug => ug.Id)
                    .FirstOrDefaultAsync()
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "DeliveryMan");

            if (!Enum.TryParse<DiscountType>(dto.DiscountType, true, out var parsedDiscountType))
            {
                throw new ArgumentException($"Invalid DiscountType value: {dto.DiscountType}");
            }

            var deliveryMan = new DeliveryMan
            {
                AppUserId = user.Id,
                VehicleNumber = dto.VehicleNumber,
                LicenseNumber = dto.LicenseNumber,
                DiscountType = parsedDiscountType,
                DiscountValue = dto.DiscountValue
            };

            _context.DeliveryMen.Add(deliveryMan);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(user.Email, "Your DeliveryMan Account",
                $"Username: {user.Email}\nPassword: {dto.Password}");

            return true;
        }

        
        public async Task<bool> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
           
            if (!await IsAdminAsync())
                throw new UnauthorizedAccessException("Only Admin can create new users.");

            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = true,
                UserType = UserType.Employee,
                Address = dto.Address,
                UserGroupId = await _context.UserGroups
                    .Where(ug => ug.Name == "Employees")
                    .Select(ug => ug.Id)
                    .FirstOrDefaultAsync()
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "Employee");

            var employee = new Employee
            {
                AppUserId = user.Id,
                EmployeeCode = dto.EmployeeCode,
                Department = dto.Department
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            await _emailService.SendEmailAsync(user.Email, "Your Employee Account",
                $"Username: {user.Email}\nPassword: {dto.Password}");

            return true;
        }
    }
}
