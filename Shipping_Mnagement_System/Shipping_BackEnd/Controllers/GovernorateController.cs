using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels;
using Shipping.Core.Services.Contracts;
using Shipping.Models;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        private readonly IGovernorateService _governorateService;

        public GovernorateController(IGovernorateService governorateService)
        {
            _governorateService = governorateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGovernorates()
        {
            var governorates = await _governorateService.GetAllGovernoratesAsync();
            return Ok(governorates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGovernorateById(int id)
        {
            var governorate = await _governorateService.GetGovernorateByIdAsync(id);
            if (governorate == null) return NotFound();
            return Ok(governorate);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGovernorate([FromBody] GovernorateDTO governorate)
        {
            var createdGovernorate = await _governorateService.CreateGovernorateAsync(governorate);
            return CreatedAtAction(nameof(GetGovernorateById), new { id = createdGovernorate.Id }, createdGovernorate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGovernorate(int id, [FromBody] GovernorateDTO governorate)
        {
            await _governorateService.UpdateGovernorateAsync(id,governorate);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ActivateDeactivateGovernorate(int id)
        {
            await _governorateService.ActivateDeactivateGovernorateAsync(id);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGovernorate(int id)
        {
            await _governorateService.DeleteGovernorateAsync(id);
            return NoContent();
        }
        [HttpGet("{id}/cities")]
        public async Task<IActionResult> GetCitiesByGovernorateId(int id)
        {
            var cities = await _governorateService.GetCitiesByGovernorateIdAsync(id);
            if (!cities.Any()) return NotFound("No cities found for this governorate.");

            return Ok(cities);
        }

    }
}
