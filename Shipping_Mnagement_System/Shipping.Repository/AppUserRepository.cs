using Shipping.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.DomainModels.Identity;

namespace Shipping.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task AddUserAsync(AppUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task AssignRoleAsync(AppUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

    }


}
