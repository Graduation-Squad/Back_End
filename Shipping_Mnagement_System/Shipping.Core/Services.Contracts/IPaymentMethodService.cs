using Shipping.Core.DomainModels.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> GetAllAsync();
        Task<PaymentMethod?> GetByIdAsync(int id);
        Task<PaymentMethod> CreateAsync(PaymentMethod method);
        Task<PaymentMethod> UpdateAsync(int id, PaymentMethod updated);
        Task<bool> ToggleStatusAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
