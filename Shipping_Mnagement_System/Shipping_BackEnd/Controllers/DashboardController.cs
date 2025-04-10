using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using Shipping_APIs.Errors;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("merchant")]
        //[Authorize(Roles = "Merchant")]
        public async Task<ActionResult<MerchantDashboardDto>> GetMerchantDashboard()
        {
            var merchantId = 1; // Replace with actual merchant ID from the token or context
            try
            {
                var result = await _dashboardService.GetMerchantDashboardAsync(merchantId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpGet("employee")]
        //[Authorize(Roles = "Employee")]
        public async Task<ActionResult<EmployeeDashboardDto>> GetEmployeeDashboard()
        {
            try
            {
                var employeeId = 1;
                var result = await _dashboardService.GetEmployeeDashboardAsync(employeeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
           
        }

        [HttpGet("admin")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<AdminDashboardDto>> GetAdminDashboard()
        {
            try
            {
                var result = await _dashboardService.GetAdminDashboardAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

    }
}
