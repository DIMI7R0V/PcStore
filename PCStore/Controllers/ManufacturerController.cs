using Microsoft.AspNetCore.Mvc;
using PCStore.BL.Interfaces;
using PCStore.Models.Models;

namespace PCStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetManufacturer(string id)
        {
            var manufacturer = await _manufacturerService.GetManufacturer(id);
            if (manufacturer == null)
            {
                return NotFound("Manufacturer not found.");
            }
            return Ok(manufacturer);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Add")]
        public async Task<IActionResult> AddManufacturer([FromBody] Manufacturer manufacturer)
        {
            await _manufacturerService.AddManufacturer(manufacturer);
            return CreatedAtAction(nameof(GetManufacturer), new { id = manufacturer.Id }, manufacturer);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteManufacturer(string id)
        {
            var result = await _manufacturerService.DeleteManufacturer(id);

            if (result)
                return NoContent();
            return NotFound($"Manufacturer with ID {id} not found.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateManufacturer([FromBody] Manufacturer manufacturer)
        {
            var result = await _manufacturerService.UpdateManufacturer(manufacturer);
            if (result)
                return NoContent();
            return NotFound($"Manufacturer with ID {manufacturer.Id} not found.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllManufacturers")]
        public async Task<IActionResult> GetAllManufacturers()
        {
            var manufacturers = await _manufacturerService.GetAllManufacturers();
            return Ok(manufacturers);
        }
    }
}
