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
    //public class OrderService : IOrderService
    //{
    //    private readonly IUnitOfWork _unitOfWork;

    //    public OrderService(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }
    //    public Task AssignOrderToDeliveryManAsync(int orderId, int deliveryManId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Order> CreateOrderAsync(OrderCreateDto orderCreateDto)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Order> GetOrderByIdAsync(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public async Task<IReadOnlyList<Order>> GetOrdersAsync(OrderParameters orderParameters)
    //    {
    //        var spec = new OrderSpecifications(orderParameters);
    //        return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
    //    }

    //    public Task<IReadOnlyList<Order>> GetOrdersByDeliveryManAsync(int deliveryManId, OrderParameters orderParameters)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IReadOnlyList<Order>> GetOrdersByMerchantAsync(int merchantId, OrderParameters orderParameters)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDto statusUpdateDto)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(OrderParameters orderParameters)
        {
            var spec = new OrderSpecifications(orderParameters);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto dto)
        {
            var order = new Order
            {
                OrderNumber = Guid.NewGuid().ToString().Substring(0, 8),
                MerchantId = dto.MerchantId,
                AreaId = dto.AreaId,
                CityId = dto.CityId,
                GovernorateId = dto.GovernorateId,
                PaymentMethodId = dto.PaymentMethodId,
                ShippingTypeId = dto.DeliveryOptionId,
                BranchId = dto.BranchId,
                TotalWeight = dto.TotalWeight,
                CODAmount = dto.CODAmount,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.CompleteAsync();

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var spec = new OrderSpecifications(id);
            return await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
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
            if (dto.CODAmount.HasValue) order.CODAmount = dto.CODAmount.Value;
            if (!string.IsNullOrEmpty(dto.Notes)) order.Notes = dto.Notes;

            order.UpdatedAt = DateTime.UtcNow;

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
    }

}
