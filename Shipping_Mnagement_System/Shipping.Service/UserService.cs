    using Microsoft.AspNetCore.Identity;
using Shipping.Core;
using Shipping.Core.Models;
using Shipping.Core.Models.Identity;
using Shipping.Core.Repositories;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using Shipping.Repository.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<DeliveryMan> RegisterDeliveryManAsync(DeliveryManRegistrationModel model)
        {
            //create appuser
            var appUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);

            //assign role
            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault().Description);

            //create deliveryman record
            var deliveryman = new DeliveryMan
            {
                AppUserId = appUser.Id,
                VehicleNumber = model.VehicleNumber,
                LicenseNumber = model.LicenseNumber
            };
            await _unitOfWork.Repository<DeliveryMan>().AddAsync(deliveryman);

            //save changes
            await _unitOfWork.CompleteAsync();

            //return deliveryman
            return deliveryman; 
        }

        public async Task<Employee> RegisterEmployeeAsync(EmployeeRegistrationModel model)
        {

            //create appuser and assign role
            var appUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault().Description);

            //assign role
            await _userManager.AddToRoleAsync(appUser, "Employee");

            //create employee record
            var employee = new Employee
            {
                AppUserId = appUser.Id,
                EmployeeCode = model.EmployeeCode,
                Department = model.Department
            };
            await _unitOfWork.Repository<Employee>().AddAsync(employee);

            //save changes
            await _unitOfWork.CompleteAsync();

            return employee;
      
            
        }

        public async Task<Merchant> RegisterMerchantAsync(MerchantRegistrationModel model)
        {
            //create appuser 
            var appUser = new AppUser
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault().Description);

            //assign role
            await _userManager.AddToRoleAsync(appUser, "Merchant");

            //create merchant record
            var merchant = new Merchant
            {
                AppUserId = appUser.Id,
                StoreName = model.StoreName,
                StoreAddress = model.StoreAddress
            };
            await _unitOfWork.Repository<Merchant>().AddAsync(merchant);

            //save changes
            await _unitOfWork.CompleteAsync();

            //return merchant   
            return merchant;
        }
    }
}
