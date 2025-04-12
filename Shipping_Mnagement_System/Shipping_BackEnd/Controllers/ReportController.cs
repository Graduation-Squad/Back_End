using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.Contracts;
using Shipping.Core.Specification;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Employee")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // Generate Orders Report
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrdersReport([FromQuery] OrderParameters parameters)
        {
            var report = await _reportService.GetOrdersReport(parameters);
            return Ok(report);
        }

        // Generate Delivery Performance Report
        [HttpGet("delivery-performance")]
        public async Task<IActionResult> GetDeliveryPerformanceReport()
        {
            var report = await _reportService.GetDeliveryPerformanceReport();
            return Ok(report);
        }

        // Generate Financial Report
        [HttpGet("financial")]
        public async Task<IActionResult> GetFinancialReport()
        {
            var report = await _reportService.GetFinancialReport();
            return Ok(report);
        }

        // Generate Merchant Activity Summary
        [HttpGet("merchant-summary")]
        public async Task<IActionResult> GetMerchantSummaryReport()
        {
            var report = await _reportService.GetMerchantSummaryReport();
            return Ok(report);
        }
    }
}
