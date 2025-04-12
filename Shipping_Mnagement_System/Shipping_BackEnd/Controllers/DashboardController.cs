using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Permissions;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using Shipping_APIs.Attributes;
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
        [Permission(Permissions.Dashboard.ViewMerchant)]
        [Authorize(Roles = "Merchant")]
        public async Task<ActionResult<MerchantDashboardDto>> GetMerchantDashboard()
        {
            try
            {
                var result = await _dashboardService.GetMerchantDashboardAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpGet("employee")]
        [Permission(Permissions.Dashboard.ViewEmployee)]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<EmployeeDashboardDto>> GetEmployeeDashboard()
        {
            try
            {
                var result = await _dashboardService.GetEmployeeDashboardAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpGet("admin")]
        [Permission(Permissions.Dashboard.ViewAdmin)]
        [Authorize(Roles = "Admin")]
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
