using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.Contracts;
using Shipping.Models;

namespace Shipping_APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("create-merchant")]
        public async Task<IActionResult> CreateMerchant(CreateMerchantDto dto)
        {
            var result = await _adminService.CreateMerchantAsync(dto);
            return result ? Ok("Merchant created.") : BadRequest("Error creating merchant.");
        }

         

        [HttpPost("create-deliveryman")]
        public async Task<IActionResult> CreateDeliveryMan(CreateDeliveryManDto dto)
        {
            try
            {
                var result = await _adminService.CreateDeliveryManAsync(dto);
                return result ? Ok("DeliveryMan created.") : BadRequest("Error creating deliveryman.");
            }
            catch (Exception ex)
            {
                return BadRequest($"❌ Exception: {ex.Message}");  
            }
        }





    }

}
