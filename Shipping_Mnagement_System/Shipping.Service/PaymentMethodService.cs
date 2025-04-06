using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
        {
            return await _unitOfWork.Repository<PaymentMethod>().GetAllAsync();
        }

        public async Task<PaymentMethod?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<PaymentMethod>().GetByIdAsync(id);
        }

        public async Task<PaymentMethod> CreateAsync(PaymentMethod method)
        {
            await _unitOfWork.Repository<PaymentMethod>().AddAsync(method);
            await _unitOfWork.CompleteAsync();
            return method;
        }

        public async Task<PaymentMethod> UpdateAsync(int id, PaymentMethod updated)
        {
            var existing = await _unitOfWork.Repository<PaymentMethod>().GetByIdAsync(id);
            if (existing == null) throw new Exception("Payment method not found");

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.IsActive = updated.IsActive;

            _unitOfWork.Repository<PaymentMethod>().Update(existing);
            await _unitOfWork.CompleteAsync();
            return existing;
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var method = await _unitOfWork.Repository<PaymentMethod>().GetByIdAsync(id);
            if (method == null) return false;

            method.IsActive = !method.IsActive;
            _unitOfWork.Repository<PaymentMethod>().Update(method);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var method = await _unitOfWork.Repository<PaymentMethod>().GetByIdAsync(id);
            if (method == null) return false;

            _unitOfWork.Repository<PaymentMethod>().Delete(method);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
