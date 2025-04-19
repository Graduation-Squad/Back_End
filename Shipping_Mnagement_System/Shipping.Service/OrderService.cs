using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Core.Specification;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Service
{

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVillageDeliveryService _villageDeliveryService;

        public OrderService(IUnitOfWork unitOfWork, IVillageDeliveryService villageDeliveryService)
        {
            _unitOfWork = unitOfWork;
            _villageDeliveryService = villageDeliveryService;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(OrderParameters orderParameters)
        {
            var spec = new OrderSpecifications(orderParameters);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto dto, string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new Exception("User email is missing or invalid.");

            
            var merchant = await _unitOfWork.Repository<Merchant>()
                .SingleOrDefaultAsync(m => m.AppUser.Email == userEmail);

            if (merchant == null)
                throw new Exception("Merchant not found.");

            decimal shippingCost = await calculateShippingCost(
                dto.TotalWeight,
                dto.GovernorateId,
                dto.CityId,
                dto.DeliveryOptionId,
                dto.IsVillageDelivery
            );

            var order = new Order
            {
                OrderNumber = Guid.NewGuid().ToString("N").Substring(0, 8), 
                MerchantId = merchant.Id,
                AreaId = dto.AreaId,
                CityId = dto.CityId,
                GovernorateId = dto.GovernorateId,
                PaymentMethodId = dto.PaymentMethodId,
                ShippingTypeId = dto.DeliveryOptionId,
                BranchId = dto.BranchId,
                TotalWeight = dto.TotalWeight,
                ShippingCost = shippingCost,
                CODAmount = shippingCost,
                Notes = dto.Notes
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }




        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var spec = new OrderSpecifications(id);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);

            if (order == null)
                throw new Exception($"Order with ID {id} not found.");

            return order;
        }


        public async Task UpdateOrderAsync(int id, OrderUpdateDto dto)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (order == null) throw new Exception("Order not found.");

            if (dto.AreaId.HasValue) order.AreaId = dto.AreaId.Value;
            if (dto.CityId.HasValue) order.CityId = dto.CityId.Value;
            if (dto.GovernorateId.HasValue) order.GovernorateId = dto.GovernorateId.Value;
            if (dto.TotalWeight.HasValue) order.TotalWeight = dto.TotalWeight.Value;
            if (dto.PaymentMethodId.HasValue) order.PaymentMethodId = dto.PaymentMethodId.Value;
            if (dto.DeliveryOptionId.HasValue) order.ShippingTypeId = dto.DeliveryOptionId.Value;
            if (dto.IsVillageDelivery.HasValue) order.IsVillageDelivery = dto.IsVillageDelivery.Value;
            if (!string.IsNullOrEmpty(dto.Notes)) order.Notes = dto.Notes;

           if(dto.TotalWeight.HasValue || dto.DeliveryOptionId.HasValue || dto.GovernorateId.HasValue || dto.IsVillageDelivery.HasValue)
           {
                decimal shippingCost = await calculateShippingCost(order.TotalWeight, order.GovernorateId, order.CityId, order.ShippingTypeId, order.IsVillageDelivery);
                order.ShippingCost = shippingCost;
                order.CODAmount = shippingCost;
           }
           
            order.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDto dto)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (order == null) throw new Exception("Order not found.");

            if (!Enum.TryParse(dto.Status, out OrderStatus parsedStatus))
                throw new Exception("Invalid status");

            order.Status = parsedStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
        }

        public async Task AssignOrderToDeliveryManAsync(int orderId, int deliveryManId)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null) throw new Exception("Order not found.");

            order.DeliveryAgentId = deliveryManId;
            order.Status = OrderStatus.Assigned;
            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByMerchantAsync(int merchantId, OrderParameters parameters)
        {
            var spec = new OrderSpecifications(parameters);
            spec.AddCriteria(o => o.MerchantId == merchantId);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByDeliveryManAsync(int deliveryManId, OrderParameters parameters)
        {
            var spec = new OrderSpecifications(parameters);
            spec.AddCriteria(o => o.DeliveryAgentId == deliveryManId);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }

        public async Task<decimal> calculateShippingCost(decimal weight, int governateId, int cityId, int shippingTypeId, bool IsVillageDelivery)
        {
            var weightSetting = await _unitOfWork.Repository<WeightSetting>().GetByIdAsync(governateId);
            if (weightSetting == null)
            {
                throw new Exception($"Weight setting for governorate ID {governateId} not found.");
            }

            var city = await _unitOfWork.Repository<City>().GetByIdAsync(cityId);
            if (city == null)
            {
                throw new Exception($"City with ID {cityId} not found.");
            }

            var shippingType = await _unitOfWork.Repository<ShippingType>().GetByIdAsync(shippingTypeId);
            if (shippingType == null)
            {
                throw new Exception("Shipping type not found.");
            }

            decimal villageExtra = (IsVillageDelivery is true) ? (decimal) await _villageDeliveryService.GetVillageDeliveryCostAsync() : 0;
            decimal shippingTypeCost = shippingType.AdditionalCost;
            decimal baseWeight = weightSetting.BaseWeight;
            decimal baseWeightCost = weightSetting.BaseWeightPrice;
            decimal additionalWeightCost = weightSetting.AdditionalWeightPrice;
            decimal deliveryToCityCost = city.DefaultShippingCost;

            if (weight <= baseWeight)
            {
                return baseWeightCost + deliveryToCityCost + shippingTypeCost + villageExtra;
            }
            else
            {
                decimal additionalWeight = weight - baseWeight;
                decimal additionalCost = Math.Ceiling(additionalWeight) * additionalWeightCost;
                return baseWeightCost + additionalCost + deliveryToCityCost + shippingTypeCost + villageExtra;
            }
        }

        public async Task<Merchant> GetMerchantByEmailAsync(string email)
        {
            var merchant = await _unitOfWork.Repository<Merchant>()
                .SingleOrDefaultAsync(m => m.AppUser.Email == email);

            if (merchant == null)
                throw new Exception($"Merchant with email {email} not found.");

            return merchant;
        }

        public async Task<DeliveryMan> GetDeliveryManByEmailAsync(string email)
        {
            var deliveryMan = await _unitOfWork.Repository<DeliveryMan>()
                .SingleOrDefaultAsync(d => d.AppUser.Email == email);

            if (deliveryMan == null)
                throw new Exception($"DeliveryMan with email {email} not found.");

            return deliveryMan;
        }



    }

}
