using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using Shipping.Service;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shipping_APIs.Controllers
{
    [ApiController]
    [Route("api/v1/orders/{orderId}/tracking")]
    public class OrderTrackingController : ControllerBase
    {
        private readonly IOrderTrackingService _service;

         
        public OrderTrackingController(IOrderTrackingService service)
        {
            _service = service;
        }

         
        [HttpGet]
        public async Task<IActionResult> GetHistory(int orderId)
        {
            try
            {
                var result = await _service.GetTrackingHistoryAsync(orderId, User);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]  
        public async Task<IActionResult> AddTracking(int orderId, CreateOrderTrackingDto dto)
        {
            try
            {
                 
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User not authorized to add tracking.");
                }

                await _service.AddTrackingEntryAsync(orderId, dto, userId);
                return Ok("Tracking entry added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Invalid input: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
