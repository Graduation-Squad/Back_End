using Microsoft.AspNetCore.Identity;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginModel model);
        Task<bool> HasPermissionAsync(string userId, string permissionName);
    }
}
