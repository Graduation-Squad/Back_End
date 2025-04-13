using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IOrderTrackingService
    {
        Task<List<OrderTrackingDto>> GetTrackingHistoryAsync(int orderId, ClaimsPrincipal user);
        Task AddTrackingEntryAsync(int orderId, CreateOrderTrackingDto dto, string userId);
    }
}
