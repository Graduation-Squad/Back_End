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
    public class VillageDeliveryService : IVillageDeliveryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillageDeliveryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<decimal?> GetVillageDeliveryCostAsync()
        {
            var cost = (await _unitOfWork.Repository<VillageDeliveryPrice>().GetAllAsync()).FirstOrDefault()?.Price;
            if (cost == null)
            {
                throw new Exception("Village delivery cost not found.");
            }
            return cost;
        }

        public async Task UpdateVillageDeliveryCostAsync(decimal amount)
        {
            var villageDeliveryPrice = (await _unitOfWork.Repository<VillageDeliveryPrice>().GetAllAsync()).FirstOrDefault();
            if (villageDeliveryPrice == null)
            {
                villageDeliveryPrice = new VillageDeliveryPrice() { Price = amount };
                await _unitOfWork.Repository<VillageDeliveryPrice>().AddAsync(villageDeliveryPrice);
                await _unitOfWork.CompleteAsync();
                return;
            }

            villageDeliveryPrice.Price = amount;
            _unitOfWork.Repository<VillageDeliveryPrice>().Update(villageDeliveryPrice);
            await _unitOfWork.CompleteAsync();
            return;
        }
    }
}
