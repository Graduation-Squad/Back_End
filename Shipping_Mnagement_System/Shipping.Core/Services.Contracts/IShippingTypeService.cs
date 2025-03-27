using Shipping.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
        public interface IShippingTypeService
        {
            Task<ShippingType?> GetByIdAsync(int id);
            Task<IEnumerable<ShippingType>> GetAllAsync(string search, int page, int pageSize);
            Task<ShippingType> AddAsync(ShippingType shippingType);
            Task UpdateAsync(ShippingType shippingType);
            Task DeleteAsync(int id);
            Task<bool?> ToggleStatusAsync(int id);
    }

    }

