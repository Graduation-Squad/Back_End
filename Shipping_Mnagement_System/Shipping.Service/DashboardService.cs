using Microsoft.AspNetCore.Http;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shipping.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        private string? GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<AdminDashboardDto> GetAdminDashboardAsync()
        {
            var dashboardData = new AdminDashboardDto();

            dashboardData.TotalMerchants = await _unitOfWork.Repository<Merchant>().CountAsync();
            dashboardData.TotalDeliveryAgents = await _unitOfWork.Repository<DeliveryMan>().CountAsync();

            var orders = await _unitOfWork.Repository<Order>().GetAllAsync();

            dashboardData.TotalOrders = orders.Count();
            dashboardData.TotalRevenue = orders.Where(o => o.Status == OrderStatus.Delivered).Sum(o => o.ShippingCost);

            dashboardData.StatusCounts = orders
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatusCountDto
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                }).ToList();

            dashboardData.RevenueByBranch = orders
                .Where(o => o.Status == OrderStatus.Delivered)
                .GroupBy(o => o.Branch.Name)
                .Select(g => new RevenueByBranchDto
                {
                    BranchName = g.Key,
                    Revenue = g.Sum(o => o.ShippingCost)
                }).ToList();

            return dashboardData;
        }

        public async Task<EmployeeDashboardDto> GetEmployeeDashboardAsync()
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not authenticated");

            var employee = (await _unitOfWork.Repository<Employee>()
                .FindAsync(e => e.AppUserId == userId))
                .FirstOrDefault();

            if (employee == null)
                throw new Exception("Employee not found.");

            var ordersSpec = new OrdersByEmployeeSpecification(employee.Id);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(ordersSpec);

            var dashboardData = new EmployeeDashboardDto
            {
                createdOrders = orders.Count(o => o.Status == OrderStatus.Created),
                assignedOrders = orders.Count(o => o.Status == OrderStatus.Assigned),
                processingOrders = orders.Count(o => o.Status == OrderStatus.Processing),
                shippedOrders = orders.Count(o => o.Status == OrderStatus.Shipped),
                deliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered),
                returnedOrders = orders.Count(o => o.Status == OrderStatus.Returned),
                rejectedOrders = orders.Count(o => o.Status == OrderStatus.Rejected),
                cancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled),
                TotalCODCollected = orders.Where(o => o.Status == OrderStatus.Delivered).Sum(o => o.CODAmount),
                StatusCounts = orders
                    .GroupBy(o => o.Status)
                    .Select(g => new OrderStatusCountDto
                    {
                        Status = g.Key.ToString(),
                        Count = g.Count()
                    }).ToList()
            };

            return dashboardData;
        }

        public async Task<MerchantDashboardDto> GetMerchantDashboardAsync()
        {
            var userId = GetCurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not authenticated");

            var merchant = (await _unitOfWork.Repository<Merchant>()
                .FindAsync(m => m.AppUserId == userId))
                .FirstOrDefault();

            if (merchant == null)
                throw new Exception("Merchant not found");

            var ordersSpec = new OrdersByMerchantSpecification(merchant.Id);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(ordersSpec);

            var dashboardData = new MerchantDashboardDto
            {
                TotalOrders = orders.Count(),
                createdOrders = orders.Count(o => o.Status == OrderStatus.Created),
                assignedOrders = orders.Count(o => o.Status == OrderStatus.Assigned),
                processingOrders = orders.Count(o => o.Status == OrderStatus.Processing),
                shippedOrders = orders.Count(o => o.Status == OrderStatus.Shipped),
                deliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered),
                returnedOrders = orders.Count(o => o.Status == OrderStatus.Returned),
                rejectedOrders = orders.Count(o => o.Status == OrderStatus.Rejected),
                cancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled),
                TotalRevenue = orders.Where(o => o.Status == OrderStatus.Delivered).Sum(o => o.ShippingCost),
                StatusCounts = orders
                    .GroupBy(o => o.Status)
                    .Select(g => new OrderStatusCountDto
                    {
                        Status = g.Key.ToString(),
                        Count = g.Count()
                    }).ToList(),
                WeeklyStats = orders
                    .Where(o => o.CreatedAt >= DateTime.UtcNow.AddDays(-7))
                    .GroupBy(o => o.CreatedAt.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new WeeklyOrderStatsDto
                    {
                        Day = g.Key.ToString("ddd"),
                        Orders = g.Count()
                    }).ToList()
            };

            return dashboardData;
        }
    }
}
