using Shipping.Core.DomainModels;
using Shipping.Core.Repositories;
using Shipping.Core.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class GovernorateService : IGovernorateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GovernorateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Governorate>> GetAllGovernoratesAsync()
        {
            return await _unitOfWork.Repository<Governorate>().GetAllAsync();
        }

        public async Task<Governorate?> GetGovernorateByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Governorate>().GetByIdAsync(id);
        }

        public async Task<Governorate> CreateGovernorateAsync(Governorate governorate)
        {
            await _unitOfWork.Repository<Governorate>().AddAsync(governorate);
            await _unitOfWork.CompleteAsync();
            return governorate;
        }

        public async Task UpdateGovernorateAsync(Governorate governorate)
        {
            _unitOfWork.Repository<Governorate>().Update(governorate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task ActivateDeactivateGovernorateAsync(int id)
        {
            var governorate = await _unitOfWork.Repository<Governorate>().GetByIdAsync(id);
            if (governorate is null) return;

            governorate.IsActive = !governorate.IsActive;
            _unitOfWork.Repository<Governorate>().Update(governorate);
            await _unitOfWork.CompleteAsync();
        }
        public async Task DeleteGovernorateAsync(int id)
        {
            var governorate = await _unitOfWork.Repository<Governorate>().GetByIdAsync(id);
            if (governorate == null)
                throw new KeyNotFoundException("Governorate not found");

            _unitOfWork.Repository<Governorate>().Delete(governorate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesByGovernorateIdAsync(int governorateId)
        {
            return await _unitOfWork.Repository<City>().FindAsync(c => c.GovernorateId == governorateId);
        }


    }
}
