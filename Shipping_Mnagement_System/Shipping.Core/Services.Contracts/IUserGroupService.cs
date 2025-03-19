using Shipping.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IUserGroupService
    {
        Task<UserGroup> CreateUserGroupAsync(string name);
        Task<IEnumerable<UserGroup>> GetAllUserGroupsAsync();
        Task<UserGroup> GetUserGroupByIdAsync(int id);
        Task AddPermissionToUserGroupAsync(int userGroupId, int permissionId);
        Task AddPermissionsToUserGroupAsync(int userGroupId, IEnumerable<int> permissionIds); 

        Task RemovePermissionFromUserGroupAsync(int userGroupId, int permissionId);
        Task DeleteUserGroupAsync(int id);
    }
}
