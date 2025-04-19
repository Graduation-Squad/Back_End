using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.Contracts;
using Shipping_APIs.Errors;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillageDeliveryController : ControllerBase
    {
        private readonly IVillageDeliveryService _villageDeliveryService;

        public VillageDeliveryController(IVillageDeliveryService villageDeliveryService)
        {
            _villageDeliveryService = villageDeliveryService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetVillageDeliveryCost()
        {
            try
            {
                var cost = await _villageDeliveryService.GetVillageDeliveryCostAsync();
                return Ok(cost);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateVillageDeliveryCost([FromBody] decimal amount)
        {
            try
            {
                await _villageDeliveryService.UpdateVillageDeliveryCostAsync(amount);
                return Ok("Village delivery cost updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }
    }
}
