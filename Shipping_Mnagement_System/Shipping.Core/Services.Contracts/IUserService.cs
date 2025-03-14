using Microsoft.AspNetCore.Identity;
using Shipping.Core.Models;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IUserService
    {
        public Task<Employee> RegisterEmployeeAsync(EmployeeRegistrationModel model);
        public Task<Merchant> RegisterMerchantAsync(MerchantRegistrationModel model);
        public Task<DeliveryMan> RegisterDeliveryManAsync(DeliveryManRegistrationModel model);
        Task<string> LoginAsync(LoginModel model);
    }
}
