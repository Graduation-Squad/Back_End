using Microsoft.AspNetCore.Identity;
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

        public Task<DeliveryMan> RegisterDeliveryManAsync(DeliveryManRegistrationModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee?> RegisterEmployeeAsync(EmployeeRegistrationModel model)
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
            {
                throw new ValidationException(result.Errors.Select(x => x.Description).Aggregate((i, j) => i + ", " + j));
            }

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

        public Task<Merchant> RegisterMerchantAsync(MerchantRegistrationModel model)
        {
            throw new NotImplementedException();
        }
    }
}
