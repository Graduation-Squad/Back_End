using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels;
using Shipping.Core.Services.Contracts;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null) return NotFound("City not found");
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            if (city == null) return BadRequest("Invalid data");

            city.Governorate = null;

            var newCity = await _cityService.CreateCityAsync(city);
            return CreatedAtAction(nameof(GetCityById), new { id = newCity.Id }, newCity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] City updatedCity)
        {
            if (updatedCity == null) return BadRequest("Invalid data");

            updatedCity.Governorate = null;

            var city = await _cityService.UpdateCityAsync(id, updatedCity);
            return Ok(city);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ToggleCityStatus(int id)
        {
            var result = await _cityService.ToggleCityStatusAsync(id);
            if (!result) return NotFound("City not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var result = await _cityService.DeleteCityAsync(id);
            if (!result) return NotFound("City not found");

            return NoContent();
        }
        [HttpGet("by-governorate/{governorateId}")]
        public async Task<IActionResult> GetCitiesByGovernorate(int governorateId)
        {
            var cities = await _cityService.GetCitiesByGovernorateIdAsync(governorateId);
            return Ok(cities);
        }

    }
}
