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
    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderTrackingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderTrackingDto>> GetTrackingHistoryAsync(int orderId)
        {
            var spec = new OrderWithTrackingSpecification(orderId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);

            if (order == null) return new();

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

        public async Task AddTrackingEntryAsync(int orderId, CreateOrderTrackingDto dto, string userId)
        {
            // Parse the string status to the OrderStatus enum
            if (!Enum.TryParse<OrderStatus>(dto.Status, out var parsedStatus))
            {
                throw new ArgumentException($"Invalid status value: {dto.Status}");
            }

            var entry = new OrderTracking
            {
                OrderId = orderId,
                Status = parsedStatus, // Use the parsed enum value
                Notes = dto.Notes,
                RejectionReasonId = dto.RejectionReasonId,
                RejectionDetails = dto.RejectionDetails,
                Timestamp = DateTime.UtcNow,
                UserId = userId
            };

            await _unitOfWork.Repository<OrderTracking>().AddAsync(entry);

            // update current order status
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = parsedStatus; // Use the parsed enum value
                order.UpdatedAt = DateTime.UtcNow;
            }

            await _unitOfWork.CompleteAsync();
        }
    }

}
