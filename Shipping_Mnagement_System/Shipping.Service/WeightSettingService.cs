using Shipping.Core.DomainModels;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class WeightSettingService : IWeightSettingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WeightSettingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WeightSettingDto>> GetAllAsync()
        {
            var weightSettings = await _unitOfWork.Repository<WeightSetting>().GetAllAsync();
            return weightSettings.Select(w => new WeightSettingDto
            {
                Id = w.Id,
                BaseWeight = w.BaseWeight,
                BaseWeightPrice = w.BaseWeightPrice,
                AdditionalWeightPrice = w.AdditionalWeightPrice,
                GovernorateId = w.GovernorateId,
                GovernorateName = w.Governorate?.Name
            }).ToList();
        }

        public async Task<WeightSettingDto> GetByIdAsync(int id)
        {
            var setting = await _unitOfWork.Repository<WeightSetting>().GetByIdAsync(id);
            if (setting == null) return null;

            return new WeightSettingDto
            {
                Id = setting.Id,
                BaseWeight = setting.BaseWeight,
                BaseWeightPrice = setting.BaseWeightPrice,
                AdditionalWeightPrice = setting.AdditionalWeightPrice,
                GovernorateId = setting.GovernorateId,
                GovernorateName = setting.Governorate?.Name
            };
        }

        public async Task CreateAsync(CreateWeightSetting dto)
        {
            var entity = new WeightSetting
            {
                BaseWeight = dto.BaseWeight,
                BaseWeightPrice = dto.BaseWeightPrice,
                AdditionalWeightPrice = dto.AdditionalWeightPrice,
                GovernorateId = dto.GovernorateId
            };

            await _unitOfWork.Repository<WeightSetting>().AddAsync(entity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(int id, CreateWeightSetting dto)
        {
            var entity = await _unitOfWork.Repository<WeightSetting>().GetByIdAsync(id);
            if (entity == null) return;

            entity.BaseWeight = dto.BaseWeight;
            entity.BaseWeightPrice = dto.BaseWeightPrice;
            entity.AdditionalWeightPrice = dto.AdditionalWeightPrice;
            entity.GovernorateId = dto.GovernorateId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Repository<WeightSetting>().GetByIdAsync(id);
            if (entity == null) return;

            _unitOfWork.Repository<WeightSetting>().Delete(entity);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<decimal> CalculateCostAsync(int governorateId, decimal weight)
        {
            var setting = await _unitOfWork.Repository<WeightSetting>().SingleOrDefaultAsync(
                w => w.GovernorateId == governorateId
            );

            if (setting == null)
                throw new Exception("No weight setting found for governorate");

            if (weight <= setting.BaseWeight)
                return setting.BaseWeightPrice;

            var extraWeight = weight - setting.BaseWeight;
            return setting.BaseWeightPrice + (extraWeight * setting.AdditionalWeightPrice);
        }
    }
}
