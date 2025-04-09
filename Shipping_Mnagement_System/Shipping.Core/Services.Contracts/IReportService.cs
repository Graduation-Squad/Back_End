using Shipping.Core.Specification;
using Shipping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Services.Contracts
{
    public interface IReportService
    {
        Task<IReadOnlyList<OrderReportDto>> GetOrdersReport(OrderParameters parameters);
        Task<DeliveryPerformanceDto> GetDeliveryPerformanceReport();
        Task<FinancialReportDto> GetFinancialReport();
        Task<IReadOnlyList<MerchantSummaryDto>> GetMerchantSummaryReport();
    }
}
