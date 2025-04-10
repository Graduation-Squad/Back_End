using Microsoft.AspNetCore.Identity;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.DomainModels;
using Shipping.Core.Enums;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Repository.Data;

namespace Shipping.Service
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ShippingContext _context;

        public AdminService(UserManager<AppUser> userManager, IEmailService emailService, ShippingContext context)
        {
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
        }

        public async Task<bool> CreateMerchantAsync(CreateMerchantDto dto)
        {
            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = true,
                UserType = UserType.Merchant,
                Address = dto.Address
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

            try
            {
                _context.Merchants.Add(merchant);
                await _context.SaveChangesAsync();

                await _emailService.SendEmailAsync(
                    user.Email,
                    "Your Merchant Account",
                    $"Username: {user.Email}\nPassword: {dto.Password}");

                return true;
            }
            catch (Exception ex)
            {
                 
                await _userManager.DeleteAsync(user);
                throw new Exception($"Failed to complete merchant creation: {ex.Message}");
            }
        }


        public async Task<bool> CreateDeliveryManAsync(CreateDeliveryManDto dto)
        {
            var user = new AppUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = true,
                UserType = UserType.DeliveryAgent,
                Address = dto.Address
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



    }

}
