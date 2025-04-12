using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels;
using Shipping.Core.Permissions;
using Shipping.Core.Services.Contracts;
using Shipping_APIs.Attributes;

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
        [Permission(Permissions.Locations.ViewCities)]

        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        [Permission(Permissions.Locations.ViewCities)]
        public async Task<IActionResult> GetCityById(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null) return NotFound("City not found");
            return Ok(city);
        }

        [HttpPost]
        [Permission(Permissions.Locations.ManageCities)]
        public async Task<IActionResult> CreateCity([FromBody] City city)
        {
            if (city == null) return BadRequest("Invalid data");

            city.Governorate = null;

            var newCity = await _cityService.CreateCityAsync(city);
            return CreatedAtAction(nameof(GetCityById), new { id = newCity.Id }, newCity);
        }

        [HttpPut("{id}")]
        [Permission(Permissions.Locations.ManageCities)]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] City updatedCity)
        {
            if (updatedCity == null) return BadRequest("Invalid data");

            updatedCity.Governorate = null;

            var city = await _cityService.UpdateCityAsync(id, updatedCity);
            return Ok(city);
        }

        [HttpPut("{id}/status")]
        [Permission(Permissions.Locations.ManageCities)]
        public async Task<IActionResult> ToggleCityStatus(int id)
        {
            var result = await _cityService.ToggleCityStatusAsync(id);
            if (!result) return NotFound("City not found");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Permission(Permissions.Locations.ManageCities)]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var result = await _cityService.DeleteCityAsync(id);
            if (!result) return NotFound("City not found");

            return NoContent();
        }
        [HttpGet("by-governorate/{governorateId}")]
        [Permission(Permissions.Locations.ViewCities)]
        public async Task<IActionResult> GetCitiesByGovernorate(int governorateId)
        {
            var cities = await _cityService.GetCitiesByGovernorateIdAsync(governorateId);
            return Ok(cities);
        }

    }
}
