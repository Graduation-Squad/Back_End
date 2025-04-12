using Shipping.Core.DomainModels;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IGovernorateService
    {
        Task<IEnumerable<Governorate>> GetAllGovernoratesAsync();
        Task<Governorate?> GetGovernorateByIdAsync(int id);
        Task<Governorate> CreateGovernorateAsync(GovernorateDTO governorate);
        Task UpdateGovernorateAsync(int id, GovernorateDTO governorate);
        Task ActivateDeactivateGovernorateAsync(int id);
        Task DeleteGovernorateAsync(int id);
        Task<IEnumerable<City>> GetCitiesByGovernorateIdAsync(int governorateId);


    }
}
