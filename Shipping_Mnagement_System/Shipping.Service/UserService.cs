//    using Microsoft.AspNetCore.Identity;
//using Shipping.Core;
//using Shipping.Core.Models;
//using Shipping.Core.Models.Identity;
//using Shipping.Core.Repositories;
//using Shipping.Core.Services.Contracts;
//using Shipping.Models;
//using Shipping.Repository.Data;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Shipping.Service
//{
//    public class UserService : IUserService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly UserManager<AppUser> _userManager;

//        public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
//        {
//            _unitOfWork = unitOfWork;
//            _userManager = userManager;
//        }

//        public async Task<DeliveryMan> RegisterDeliveryManAsync(DeliveryManRegistrationModel model)
//        {
//            //create appuser
//            var appUser = new AppUser
//            {
//                Email = model.Email,
//                UserName = model.Email.Split('@')[0],
//                FullName = model.FullName,
//                PhoneNumber = model.PhoneNumber,
//                Address = model.Address
//            };
//            var result = await _userManager.CreateAsync(appUser, model.Password);

//            //assign role
//            if (!result.Succeeded)
//                throw new Exception(result.Errors.FirstOrDefault().Description);

//            //create deliveryman record
//            var deliveryman = new DeliveryMan
//            {
//                AppUserId = appUser.Id,
//                VehicleNumber = model.VehicleNumber,
//                LicenseNumber = model.LicenseNumber
//            };
//            await _unitOfWork.Repository<DeliveryMan>().AddAsync(deliveryman);

//            //save changes
//            await _unitOfWork.CompleteAsync();

//            //return deliveryman
//            return deliveryman; 
//        }

//        public async Task<Employee> RegisterEmployeeAsync(EmployeeRegistrationModel model)
//        {

//            //create appuser and assign role
//            var appUser = new AppUser
//            {
//                Email = model.Email,
//                UserName = model.Email.Split('@')[0],
//                FullName = model.FullName,
//                PhoneNumber = model.PhoneNumber,
//                Address = model.Address
//            };
//            var result = await _userManager.CreateAsync(appUser, model.Password);

//            if (!result.Succeeded)
//                throw new Exception(result.Errors.FirstOrDefault().Description);

//            //assign role
//            await _userManager.AddToRoleAsync(appUser, "Employee");

//            //create employee record
//            var employee = new Employee
//            {
//                AppUserId = appUser.Id,
//                EmployeeCode = model.EmployeeCode,
//                Department = model.Department
//            };
//            await _unitOfWork.Repository<Employee>().AddAsync(employee);

//            //save changes
//            await _unitOfWork.CompleteAsync();

//            return employee;


//        }

//        public async Task<Merchant> RegisterMerchantAsync(MerchantRegistrationModel model)
//        {
//            //create appuser 
//            var appUser = new AppUser
//            {
//                Email = model.Email,
//                UserName = model.Email.Split('@')[0],
//                FullName = model.FullName,
//                PhoneNumber = model.PhoneNumber,
//                Address = model.Address
//            };
//            var result = await _userManager.CreateAsync(appUser, model.Password);
//            if (!result.Succeeded)
//                throw new Exception(result.Errors.FirstOrDefault().Description);

//            //assign role
//            await _userManager.AddToRoleAsync(appUser, "Merchant");

//            //create merchant record
//            var merchant = new Merchant
//            {
//                AppUserId = appUser.Id,
//                StoreName = model.StoreName,
//                StoreAddress = model.StoreAddress
//            };
//            await _unitOfWork.Repository<Merchant>().AddAsync(merchant);

//            //save changes
//            await _unitOfWork.CompleteAsync();

//            //return merchant   
//            return merchant;
//        }
//    }
//}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.Enums;
using Shipping.Core.Models;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (!Enum.TryParse(request.UserType, out UserType userType))
            {
                throw new Exception("Invalid UserType.");
            }

            if (userType == UserType.Employee)
            {
                var userGroup = await _unitOfWork.Repository<UserGroup>().GetByIdAsync(request.UserGroupId.Value);
                if (userGroup == null)
                {
                    throw new Exception("UserGroup not found.");
                }
            }

            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                throw new Exception("Email is already registered.");
            }


            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.Email.Split("@")[0],
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                UserType = Enum.Parse<UserType>(request.UserType),
                UserGroupId = (Enum.Parse<UserType>(request.UserType) == UserType.Employee) ? request.UserGroupId : null    
            };

            var result =await  _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault().Description);

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Username = user.UserName,
                UserType = user.UserType.ToString()
            };
        }
        public async Task<string> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid email or password");

            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Add role claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
