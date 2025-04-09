using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using System.Security.Claims;

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
            var result = await _service.GetTrackingHistoryAsync(orderId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> AddTracking(int orderId, CreateOrderTrackingDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.AddTrackingEntryAsync(orderId, dto, userId);
            return Ok();
        }
    }

}
