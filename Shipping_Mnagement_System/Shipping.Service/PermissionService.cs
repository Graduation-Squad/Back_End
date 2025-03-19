using Shipping.Core.DomainModels;
using Shipping.Core.Repositories;
using Shipping.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;   
        public PermissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Permission> CreatePermissionAsync(string name, string description, string module)
        {
            var Permission = new Permission
            {
                Name = name,
                Description = description,
                Module = module
            };

            await _unitOfWork.Repository<Permission>().AddAsync(Permission);
            await _unitOfWork.CompleteAsync();

            return Permission;
        }

        public async Task DeletePermissionAsync(int id)
        {
            var Permission = await _unitOfWork.Repository<Permission>().GetByIdAsync(id);
            if(Permission != null)
            {
                 _unitOfWork.Repository<Permission>().Delete(Permission);
            }
            await _unitOfWork.CompleteAsync();
        }

        public Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return _unitOfWork.Repository<Permission>().GetAllAsync();
        }

        public Task<Permission> GetPermissionByIdAsync(int id)
        {
            return _unitOfWork.Repository<Permission>().GetByIdAsync(id);
        }
    }
}
