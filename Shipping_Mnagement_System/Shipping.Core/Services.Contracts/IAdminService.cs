using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IAdminService
    {
        Task<bool> CreateMerchantAsync(CreateMerchantDto dto);
        Task<bool> CreateDeliveryManAsync(CreateDeliveryManDto dto);
        Task<bool> CreateEmployeeAsync(CreateEmployeeDto dto);

    }

}
