using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using Shipping.Core.Repositories.Contracts;
using Shipping.Core.Services.Contracts;
using Shipping.Core.Specification;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Orders Report
        public async Task<IReadOnlyList<OrderReportDto>> GetOrdersReport(OrderParameters parameters)
        {
            var spec = new OrderSpecifications(parameters);  // Using existing OrderSpecifications
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders.Select(order => new OrderReportDto
            {
                OrderId = order.Id,
                MerchantName = order.Merchant.StoreName,
                Status = order.Status.ToString(),
                CreatedAt = order.CreatedAt,
                TotalAmount = order.CODAmount + order.ShippingCost,  // Assuming COD + Shipping cost is the total revenue
                ShippingCost = order.ShippingCost
            }).ToList();
        }

        // Delivery Performance Report
        public async Task<DeliveryPerformanceDto> GetDeliveryPerformanceReport()
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

            int totalDelivered = orders.Count(o => o.Status == OrderStatus.Delivered);
            int totalRejected = orders.Count(o => o.Status == OrderStatus.Rejected);

            return new DeliveryPerformanceDto
            {
                TotalDelivered = totalDelivered,
                TotalRejected = totalRejected,
                DeliveryRate = totalDelivered == 0 ? 0 : ((double)totalDelivered / orders.Count()) * 100
            };
        }

        // Financial Report
        public async Task<FinancialReportDto> GetFinancialReport()
        {
            // This uses the correct method that accepts the predicate
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync(o => o.Status == OrderStatus.Delivered);

            var totalRevenue = orders.Sum(o => o.CODAmount);  // Assuming CODAmount is the revenue
            var totalCost = orders.Sum(o => o.ShippingCost);  // Assuming ShippingCost is the cost

            return new FinancialReportDto
            {
                TotalRevenue = totalRevenue,
                TotalCost = totalCost,
                NetProfit = totalRevenue - totalCost
            };
        }


        // Merchant Summary Report
        public async Task<IReadOnlyList<MerchantSummaryDto>> GetMerchantSummaryReport()
        {
            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

            var merchants = orders.GroupBy(o => o.MerchantId)
                .Select(g => new MerchantSummaryDto
                {
                    MerchantName = g.FirstOrDefault()?.Merchant?.StoreName ?? "Unknown Merchant",  // Null check
                    OrdersCount = g.Count(),
                    TotalRevenue = g.Sum(o => o.CODAmount)
                }).ToList();

            return merchants;
        }


    }
}
