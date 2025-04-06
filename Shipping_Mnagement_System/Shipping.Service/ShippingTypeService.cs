using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class ShippingTypeService : IShippingTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShippingTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ShippingType?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<ShippingType>().GetByIdAsync(id);
        }

        public async Task<IEnumerable<ShippingType>> GetAllAsync(string search = "", int page = 1, int pageSize = 10)
        {
            var shippingTypes = await _unitOfWork.Repository<ShippingType>().GetAllAsync();

            if (!string.IsNullOrEmpty(search))
            {
                shippingTypes = shippingTypes.Where(st => st.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return shippingTypes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<ShippingType> AddAsync(ShippingType shippingType)
        {
            await _unitOfWork.Repository<ShippingType>().AddAsync(shippingType);
            await _unitOfWork.CompleteAsync();
            return shippingType; 
        }

        public async Task UpdateAsync(ShippingType shippingType)
        {
            _unitOfWork.Repository<ShippingType>().Update(shippingType);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<ShippingType>().GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.Repository<ShippingType>().Delete(entity);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<bool?> ToggleStatusAsync(int id)
        {
            var shippingType = await _unitOfWork.Repository<ShippingType>().GetByIdAsync(id);
            if (shippingType == null) return null;

            shippingType.IsActive = !shippingType.IsActive;  
            _unitOfWork.Repository<ShippingType>().Update(shippingType);
            await _unitOfWork.CompleteAsync();

            return shippingType.IsActive;
        }

    }


}
