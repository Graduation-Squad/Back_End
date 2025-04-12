using Shipping.Core.DomainModels;
using Shipping.Core.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipping.Core.Repositories.Contracts;
using Shipping.Models;

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

        public async Task<Governorate> CreateGovernorateAsync(GovernorateDTO governorate)
        {
            Governorate NewGovernorate = new Governorate
            {
                Name = governorate.Name,
                IsActive = governorate.IsActive
            };

            await _unitOfWork.Repository<Governorate>().AddAsync(NewGovernorate);
            await _unitOfWork.CompleteAsync();

            return NewGovernorate;
        }

        public async Task UpdateGovernorateAsync(int id ,GovernorateDTO governorate)
        {
            Governorate o_governorate = await _unitOfWork.Repository<Governorate>().GetByIdAsync(id);
           
            if (governorate == null)
                throw new Exception("Governorate not found"); ;


            o_governorate.Name = governorate.Name;
            o_governorate.IsActive = governorate.IsActive;

            _unitOfWork.Repository<Governorate>().Update(o_governorate);
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
