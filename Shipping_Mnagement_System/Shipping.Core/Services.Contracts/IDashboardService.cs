using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IDashboardService
    {
        Task<MerchantDashboardDto> GetMerchantDashboardAsync();
        Task<EmployeeDashboardDto> GetEmployeeDashboardAsync();
        Task<AdminDashboardDto> GetAdminDashboardAsync();

    }
}
