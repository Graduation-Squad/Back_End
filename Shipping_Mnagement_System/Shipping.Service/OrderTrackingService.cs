using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Core.Specification;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderTrackingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get tracking history for an order
        public async Task<List<OrderTrackingDto>> GetTrackingHistoryAsync(int orderId, ClaimsPrincipal user)
        {
            var userEmail = user.FindFirstValue(ClaimTypes.Email);
            var userRole = user.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userRole))
                throw new UnauthorizedAccessException("Invalid token.");

            var spec = new OrderWithTrackingSpecification(orderId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
            if (order == null) throw new Exception("Order not found.");

            // Role-based logic
            if (userRole == "Merchant" && order.Merchant.AppUser.Email != userEmail)
                throw new UnauthorizedAccessException("You can only view your own orders.");

            if (userRole == "DeliveryMan" && order.DeliveryAgent.AppUser.Email != userEmail)
                throw new UnauthorizedAccessException("You can only view orders assigned to you.");

            // Admin or Employee can access any
            return order.OrderTrackings.OrderBy(t => t.Timestamp).Select(t => new OrderTrackingDto
            {
                Status = t.Status.ToString(),
                Notes = t.Notes,
                RejectionReason = t.RejectionReason?.Name,
                RejectionDetails = t.RejectionDetails,
                UpdatedBy = t.User?.UserName,
                Timestamp = t.Timestamp
            }).ToList();
        }


        // Add a tracking entry for an order
        public async Task AddTrackingEntryAsync(int orderId, CreateOrderTrackingDto dto, string userId)
        {
            // Parse the status string to the OrderStatus enum
            if (!Enum.TryParse<OrderStatus>(dto.Status, out var parsedStatus))
            {
                throw new ArgumentException($"Invalid status value: {dto.Status}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(OrderStatus)))}.");
            }

            var entry = new OrderTracking
            {
                OrderId = orderId,
                Status = parsedStatus,
                Notes = dto.Notes,
                RejectionReasonId = dto.RejectionReasonId,
                RejectionDetails = dto.RejectionDetails,
                Timestamp = DateTime.UtcNow,
                UserId = userId
            };

            await _unitOfWork.Repository<OrderTracking>().AddAsync(entry);

            // Update current order status
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException("Order not found.");
            }

            order.Status = parsedStatus;
            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CompleteAsync();
        }
    }
}
