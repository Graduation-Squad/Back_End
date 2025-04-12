using Shipping.Core.DomainModels;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City?> GetCityByIdAsync(int id);
        Task<City> CreateCityAsync(CityDTO city);
        Task<City> UpdateCityAsync(int id, CityDTO updatedCity);
        Task<bool> ToggleCityStatusAsync(int id);
        Task<bool> DeleteCityAsync(int id);
        Task<IEnumerable<City>> GetCitiesByGovernorateIdAsync(int governorateId);

    }
}
