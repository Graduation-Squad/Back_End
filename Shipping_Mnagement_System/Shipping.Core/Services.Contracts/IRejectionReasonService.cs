using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IRejectionReasonService
    {
        Task<IEnumerable<RejectionReason>> GetAllAsync();
        Task<IEnumerable<RejectionReason>> GetByTypeAsync(RejectionReasonType type);
        Task<RejectionReason?> GetByIdAsync(int id);
        Task<RejectionReason> CreateAsync(RejectionReason reason);
        Task<RejectionReason> UpdateAsync(int id, RejectionReason updated);
        Task<bool> ToggleStatusAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
