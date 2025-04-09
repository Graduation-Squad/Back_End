
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.Enums;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            // Parse string to enum
            if (!Enum.TryParse(request.UserType, out UserType userType))
            {
                throw new Exception("Invalid UserType.");
            }

            if (userType == UserType.Employee && !request.UserGroupId.HasValue)
            {
                throw new Exception("UserGroupId is required for Employees.");
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
                UserType = userType,  // Use the parsed enum
                UserGroupId = userType == UserType.Employee ? request.UserGroupId : null
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault()?.Description);

            // Get the role name from the enum
            var roleName = userType.ToString();

            // Validate role name is not empty  
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new InvalidOperationException("Invalid role name generated from UserType");
            }

            // Check if role exists (case-sensitive check)
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                // Create the role if it doesn't exist
                var roleCreationResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (!roleCreationResult.Succeeded)
                {
                    var errors = string.Join(", ", roleCreationResult.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create role '{roleName}': {errors}");
                }
            }

            // Assign the role to the user
            var roleAssignmentResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!roleAssignmentResult.Succeeded)
            {
                var errors = string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to assign role '{roleName}' to user: {errors}");
            }

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Username = user.UserName,
                UserType = userType.ToString()
            };
        }



        public async Task<string> LoginAsync(LoginModel model)
        {
            Console.WriteLine($"Login attempt for email: {model.Email}"); // Debug
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                Console.WriteLine("User not found"); // Debug
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                Console.WriteLine("Invalid password"); // Debug
                throw new UnauthorizedAccessException("Invalid email or password");
            }
            Console.WriteLine($"User found: {user.Email}"); // Debug
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
