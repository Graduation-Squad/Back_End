using Shipping.Core.DomainModels;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserGroup> CreateUserGroupAsync(string name)
        {
            var userGroup = new UserGroup
            {
                Name = name
            };

            await _unitOfWork.Repository<UserGroup>().AddAsync(userGroup);
            await _unitOfWork.CompleteAsync();

            return userGroup;
        }

        public async Task DeleteUserGroupAsync(int id)
        {
            var userGroup = await _unitOfWork.Repository<UserGroup>().GetByIdAsync(id);
            if (userGroup != null)
            {
                _unitOfWork.Repository<UserGroup>().Delete(userGroup);
            }
            await _unitOfWork.CompleteAsync();
        }

        public Task<IEnumerable<UserGroup>> GetAllUserGroupsAsync()
        {
            return _unitOfWork.Repository<UserGroup>().GetAllAsync();
        }

        public Task<UserGroup> GetUserGroupByIdAsync(int id)
        {
            return _unitOfWork.Repository<UserGroup>().GetByIdAsync(id);
        }

        public async Task AddPermissionToUserGroupAsync(int userGroupId, int permissionId)
        {
            var permission = await _unitOfWork.Repository<Permission>().GetByIdAsync(permissionId);
            var userGroup = await _unitOfWork.Repository<UserGroup>().GetByIdAsync(userGroupId);

            if (permission == null || userGroup == null) 
            {
                throw new Exception("Permission or UserGroup not found");
            }

            var userGroupPermission = new UserGroupPermission
            {
                UserGroupId = userGroupId,
                PermissionId = permissionId
            };

            await _unitOfWork.Repository<UserGroupPermission>().AddAsync(userGroupPermission);
            await _unitOfWork.CompleteAsync();


        }

        public async Task AddPermissionsToUserGroupAsync(int userGroupId, IEnumerable<int> permissionIds)
        {
            var userGroup = await _unitOfWork.Repository<UserGroup>().GetByIdAsync(userGroupId);
            if (userGroup is null)
                throw new Exception("userGroup not found");

            var permissions = await _unitOfWork.Repository<Permission>().GetAllAsync();
            var validPermissionIds = permissions.Select(p => p.Id).ToList();

            foreach (var permissionId in permissionIds)
            {
                if (!validPermissionIds.Contains(permissionId))
                    throw new Exception("one or more permission not found");

                await _unitOfWork.Repository<UserGroupPermission>().AddAsync(new UserGroupPermission
                {
                    PermissionId = permissionId,
                    UserGroupId = userGroupId
                });
            }

            await _unitOfWork.CompleteAsync();
        }
        public async Task RemovePermissionFromUserGroupAsync(int userGroupId, int permissionId)
        {
            var userGroupPermission = (await _unitOfWork.Repository<UserGroupPermission>().GetAllAsync())
                .FirstOrDefault(ugp => ugp.UserGroupId == userGroupId && ugp.PermissionId == permissionId);

            if (userGroupPermission != null)
                 _unitOfWork.Repository<UserGroupPermission>().Delete(userGroupPermission);

            await _unitOfWork.CompleteAsync();
        }

    }
}
