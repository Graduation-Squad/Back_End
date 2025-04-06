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
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _unitOfWork.Repository<City>().GetAllAsync();
        }

        public async Task<City?> GetCityByIdAsync(int id)
        {
            return await _unitOfWork.Repository<City>().GetByIdAsync(id);
        }

        public async Task<City> CreateCityAsync(City city)
        {
            city.Governorate = null;

            await _unitOfWork.Repository<City>().AddAsync(city);
            await _unitOfWork.CompleteAsync();
            return city;
        }

        public async Task<City> UpdateCityAsync(int id, City updatedCity)
        {
            var city = await _unitOfWork.Repository<City>().GetByIdAsync(id);
            if (city == null) throw new Exception("City not found");

            city.Name = updatedCity.Name;
            city.IsActive = updatedCity.IsActive;
            city.GovernorateId = updatedCity.GovernorateId;

            _unitOfWork.Repository<City>().Update(city);
            await _unitOfWork.CompleteAsync();
            return city;
        }

        public async Task<bool> ToggleCityStatusAsync(int id)
        {
            var city = await _unitOfWork.Repository<City>().GetByIdAsync(id);
            if (city == null) return false;

            city.IsActive = !city.IsActive;
            _unitOfWork.Repository<City>().Update(city);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var city = await _unitOfWork.Repository<City>().GetByIdAsync(id);
            if (city == null) return false;

            _unitOfWork.Repository<City>().Delete(city);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<City>> GetCitiesByGovernorateIdAsync(int governorateId)
        {
            return await _unitOfWork.Repository<City>().FindAsync(c => c.GovernorateId == governorateId);
        }


    }
}
