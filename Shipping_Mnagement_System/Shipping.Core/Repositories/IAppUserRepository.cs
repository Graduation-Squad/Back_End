using Shipping.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Repositories
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task AddUserAsync(AppUser user);
    }


}
