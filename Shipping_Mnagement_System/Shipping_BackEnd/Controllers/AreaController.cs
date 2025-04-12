    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels;
using Shipping.Core.Permissions;
using Shipping.Core.Services.Contracts;
using Shipping.Models;
using Shipping_APIs.Attributes;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        [Permission(Permissions.Locations.ViewAreas)]
        public async Task<IActionResult> GetAllAreas()
        {
            var areas = await _areaService.GetAllAreasAsync();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        [Permission(Permissions.Locations.ViewAreas)]
        public async Task<IActionResult> GetAreaById(int id)
        {
            var area = await _areaService.GetAreaByIdAsync(id);
            if (area == null) return NotFound("Area not found");
            return Ok(area);
        }

        [HttpGet("by-city/{cityId}")]
        [Permission(Permissions.Locations.ViewAreas)]
        public async Task<IActionResult> GetAreasByCityId(int cityId)
        {
            var areas = await _areaService.GetAreasByCityIdAsync(cityId);
            return Ok(areas);
        }

        [HttpPost]
        [Permission(Permissions.Locations.ManageAreas)]
        public async Task<IActionResult> CreateArea([FromBody] AreaDTO area)
        {
            if (area == null) return BadRequest("Invalid data");

            var newArea = await _areaService.CreateAreaAsync(area);
            return CreatedAtAction(nameof(GetAreaById), new { id = newArea.Id }, newArea);
        }

        [HttpPut("{id}")]
        [Permission(Permissions.Locations.ManageAreas)]
        public async Task<IActionResult> UpdateArea(int id, [FromBody] AreaDTO updatedArea)
        {
            var area = await _areaService.UpdateAreaAsync(id, updatedArea);
            return Ok(area);
        }

        [HttpPut("{id}/status")]
        [Permission(Permissions.Locations.ManageAreas)]
        public async Task<IActionResult> ToggleAreaStatus(int id)
        {
            var result = await _areaService.ToggleAreaStatusAsync(id);
            if (!result) return NotFound("Area not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Permission(Permissions.Locations.ManageAreas)]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var result = await _areaService.DeleteAreaAsync(id);
            if (!result) return NotFound("Area not found");

            return NoContent();
        }
    }
}
