using Microsoft.AspNetCore.Identity;
using Shipping.Core.Models;
using Shipping.Core.Models.Identity;
using Shipping.Models;
using Shipping.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ShippingContext _context;

        public UserService(UserManager<AppUser> userManager, ShippingContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IdentityResult> RegisterEmployeeAsync(EmployeeRegistrationModel model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                //assign role
                await _userManager.AddToRoleAsync(user, "Employee");

                //create employee record
                var employee = new Employee
                {
                    AppUserId = user.Id,
                    EmployeeCode = model.EmployeeCode,
                    Department = model.Department
                };

                _context.Add(employee);
                await _context.SaveChangesAsync();
            }

            return result;
        }

    }
}
