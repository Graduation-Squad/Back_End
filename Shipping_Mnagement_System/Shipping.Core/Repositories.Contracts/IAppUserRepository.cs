using Shipping.Core.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Repositories.Contracts
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task AddUserAsync(AppUser user, string password);
        Task AssignRoleAsync(AppUser user, string role);
    }


}
