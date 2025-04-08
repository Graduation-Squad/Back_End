using Shipping.Core.DomainModels;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IWeightSettingService
    {
        Task<List<WeightSettingDto>> GetAllAsync();
        Task<WeightSettingDto> GetByIdAsync(int id);
        Task CreateAsync(CreateWeightSetting dto);
        Task UpdateAsync(int id, CreateWeightSetting dto);
        Task DeleteAsync(int id);
        Task<decimal> CalculateCostAsync(int governorateId, decimal weight);
    }

}
