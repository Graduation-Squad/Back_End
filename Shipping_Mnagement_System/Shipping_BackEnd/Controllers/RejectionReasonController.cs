using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Enums;
using Shipping.Core.Services.Contracts;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectionReasonController : ControllerBase
    {
        private readonly IRejectionReasonService _service;

        public RejectionReasonController(IRejectionReasonService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,DeliveryMan")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,DeliveryMan")]
        public async Task<IActionResult> Get(int id)
        {
            var reason = await _service.GetByIdAsync(id);
            return reason == null ? NotFound() : Ok(reason);
        }
        

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RejectionReason reason)
        {
            var created = await _service.CreateAsync(reason);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, RejectionReason updated)
        {
            try
            {
                var result = await _service.UpdateAsync(id, updated);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/toggle")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _service.ToggleStatusAsync(id);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("type/{type}")]
        [Authorize(Roles = "Admin,DeliveryMan")]
        public async Task<IActionResult> GetByType(RejectionReasonType type)
        {
            var reasons = await _service.GetByTypeAsync(type);
            return Ok(reasons);
        }

    }
}
