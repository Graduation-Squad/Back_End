using Shipping.Core.DomainModels.OrderModels;
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

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AssignOrderToDeliveryManAsync(int orderId, int deliveryManId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(OrderParameters orderParameters)
        {
            var spec = new OrderSpecifications(orderParameters);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
        }

        public Task<IReadOnlyList<Order>> GetOrdersByDeliveryManAsync(int deliveryManId, OrderParameters orderParameters)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersByMerchantAsync(int merchantId, OrderParameters orderParameters)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDto statusUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
