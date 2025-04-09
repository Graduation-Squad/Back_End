using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IOrderTrackingService
    {
        Task<List<OrderTrackingDto>> GetTrackingHistoryAsync(int orderId);
        Task AddTrackingEntryAsync(int orderId, CreateOrderTrackingDto dto, string userId);
    }
}
