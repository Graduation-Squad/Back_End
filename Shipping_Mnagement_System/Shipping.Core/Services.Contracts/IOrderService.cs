using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Specification;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetOrdersAsync(OrderParameters orderParameters);
        Task<Order> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto);
        Task UpdateOrderStatusAsync(int id, OrderStatusUpdateDto statusUpdateDto);
        Task AssignOrderToDeliveryManAsync(int orderId, int deliveryManId);
        Task<IReadOnlyList<Order>> GetOrdersByMerchantAsync(int merchantId, OrderParameters orderParameters);
        Task<IReadOnlyList<Order>> GetOrdersByDeliveryManAsync(int deliveryManId, OrderParameters orderParameters);

    }
}
