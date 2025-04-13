using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ShippingTypeController : ControllerBase
    {
        private readonly IShippingTypeService _shippingTypeService;

        public ShippingTypeController(IShippingTypeService shippingTypeService)
        {
            _shippingTypeService = shippingTypeService;
        }

        // GET: api/ShippingType
        [HttpGet]
        [Authorize(Roles = "Admin,Merchant")]
        public async Task<ActionResult<IEnumerable<ShippingType>>> GetAll([FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var shippingTypes = await _shippingTypeService.GetAllAsync(search, page, pageSize);
            return Ok(shippingTypes);
        }

        // GET: api/ShippingType/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Merchant")]
        public async Task<ActionResult<ShippingType>> GetById(int id)
        {
            var shippingType = await _shippingTypeService.GetByIdAsync(id);
            if (shippingType == null)
            {
                return NotFound(new { message = "Shipping type not found" });
            }
            return Ok(shippingType);
        }

        // POST: api/ShippingType
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ShippingType shippingType)
        {
            if (shippingType == null)
            {
                return BadRequest(new { message = "Invalid shipping type data." });
            }

            var createdShippingType = await _shippingTypeService.AddAsync(shippingType);
            return CreatedAtAction(nameof(GetById), new { id = createdShippingType.Id }, createdShippingType);
        }

        // PUT: api/ShippingType/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ShippingType shippingType)
        {
            if (shippingType == null || id != shippingType.Id)
            {
                return BadRequest(new { message = "Shipping type data is incorrect." });
            }

            var existingShippingType = await _shippingTypeService.GetByIdAsync(id);
            if (existingShippingType == null)
            {
                return NotFound(new { message = "Shipping type not found" });
            }

            await _shippingTypeService.UpdateAsync(shippingType);
            return Ok(new { message = "Shipping type updated successfully" });
        }

        // DELETE: api/ShippingType/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var shippingType = await _shippingTypeService.GetByIdAsync(id);
            if (shippingType == null)
            {
                return NotFound(new { message = "Shipping type not found" });
            }

            await _shippingTypeService.DeleteAsync(id);
            return Ok(new { message = "Shipping type deleted successfully" });
        }

        // PATCH: api/ShippingType/{id}/toggle-status
        [HttpPatch("{id}/toggle-status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var updatedStatus = await _shippingTypeService.ToggleStatusAsync(id);
            if (updatedStatus == null)
            {
                return NotFound(new { message = "Shipping type not found" });
            }

            return Ok(new { message = "Shipping type status updated successfully", newStatus = updatedStatus });
        }
    }
}
