using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class RejectionReasonService : IRejectionReasonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RejectionReasonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RejectionReason>> GetAllAsync()
        {
            return await _unitOfWork.Repository<RejectionReason>().GetAllAsync();
        }
        public async Task<IEnumerable<RejectionReason>> GetByTypeAsync(RejectionReasonType type)
        {
            return await _unitOfWork.Repository<RejectionReason>()
                .FindAsync(r => r.Type == type);
        }

        public async Task<RejectionReason?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<RejectionReason>().GetByIdAsync(id);
        }

        public async Task<RejectionReason> CreateAsync(RejectionReason reason)
        {
            await _unitOfWork.Repository<RejectionReason>().AddAsync(reason);
            await _unitOfWork.CompleteAsync();
            return reason;
        }

        public async Task<RejectionReason> UpdateAsync(int id, RejectionReason updated)
        {
            var reason = await _unitOfWork.Repository<RejectionReason>().GetByIdAsync(id);
            if (reason == null) throw new Exception("Rejection reason not found");

            reason.Name = updated.Name;
            reason.Description = updated.Description;
            reason.RequiresDetails = updated.RequiresDetails;
            reason.IsActive = updated.IsActive;

            _unitOfWork.Repository<RejectionReason>().Update(reason);
            await _unitOfWork.CompleteAsync();
            return reason;
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var reason = await _unitOfWork.Repository<RejectionReason>().GetByIdAsync(id);
            if (reason == null) return false;

            reason.IsActive = !reason.IsActive;
            _unitOfWork.Repository<RejectionReason>().Update(reason);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reason = await _unitOfWork.Repository<RejectionReason>().GetByIdAsync(id);
            if (reason == null) return false;

            _unitOfWork.Repository<RejectionReason>().Delete(reason);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
