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
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public async Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(int employeeId)
        {
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(employeeId);
            if (employee == null)
                throw new Exception($"employee with id:{employeeId} not found.");

            var dashboardData = new EmployeeDashboardDto();

            var ordersSpec = new OrdersByEmployeeSpecification(employeeId);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(ordersSpec);

            dashboardData.createdOrders = orders.Count(o => o.Status == OrderStatus.Created);
            dashboardData.assignedOrders = orders.Count(o => o.Status == OrderStatus.Assigned);
            dashboardData.processingOrders = orders.Count(o => o.Status == OrderStatus.Processing);
            dashboardData.shippedOrders = orders.Count(o => o.Status == OrderStatus.Shipped);
            dashboardData.deliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered);
            dashboardData.returnedOrders = orders.Count(o => o.Status == OrderStatus.Returned);
            dashboardData.rejectedOrders = orders.Count(o => o.Status == OrderStatus.Rejected);
            dashboardData.cancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled);

            dashboardData.TotalCODCollected = orders.Where(o => o.Status == OrderStatus.Delivered).Sum(o => o.CODAmount);

            dashboardData.StatusCounts = orders
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatusCountDto
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                }).ToList();

            return dashboardData;
        }

        public async Task<MerchantDashboardDto> GetMerchantDashboardAsync(int merchantId)
        {
            var merchant = await  _unitOfWork.Repository<Merchant>().GetByIdAsync(merchantId);
            if(merchant is null)
                throw new Exception($"Merchant with id: {merchantId} not found");

            var ordersSpec = new OrdersByMerchantSpecification(merchantId);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(ordersSpec);

            var dashboardData = new MerchantDashboardDto();

            dashboardData.TotalOrders = orders.Count();

            dashboardData.createdOrders = orders.Count(o => o.Status == OrderStatus.Created);
            dashboardData.assignedOrders = orders.Count(o => o.Status == OrderStatus.Assigned);
            dashboardData.processingOrders = orders.Count(o => o.Status == OrderStatus.Processing);
            dashboardData.shippedOrders = orders.Count(o => o.Status == OrderStatus.Shipped);
            dashboardData.deliveredOrders = orders.Count(o => o.Status == OrderStatus.Delivered);
            dashboardData.returnedOrders = orders.Count(o => o.Status == OrderStatus.Returned);
            dashboardData.rejectedOrders = orders.Count(o => o.Status == OrderStatus.Rejected);
            dashboardData.cancelledOrders = orders.Count(o => o.Status == OrderStatus.Cancelled);

            dashboardData.TotalRevenue = orders.Where(o => o.Status == OrderStatus.Delivered)
                .Sum(o => o.ShippingCost);

            dashboardData.StatusCounts = orders
                .GroupBy(o => o.Status)
                .Select(g => new OrderStatusCountDto
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                }).ToList();

            var weeklyOrders = orders
                .Where(o => o.CreatedAt >= DateTime.UtcNow.AddDays(-7))
                .ToList();


            dashboardData.WeeklyStats = weeklyOrders
                .GroupBy(o => o.CreatedAt.Date)
                .OrderBy(g => g.Key)
                .Select(g => new WeeklyOrderStatsDto
                {
                    Day = g.Key.ToString("ddd"),
                    Orders = g.Count()
                }).ToList();

            return dashboardData;
        }
    }
}
