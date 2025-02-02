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
        public IActionResult GetManufacturer(string id)
        {
            var manufacturer = _manufacturerService.GetManufacturer(id);
            if (manufacturer == null)
            {
                return NotFound("Manufacturer not found.");
            }
            return Ok(manufacturer);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Add")]
        public IActionResult AddManufacturer(Manufacturer manufacturer)
        {
            _manufacturerService.AddManufacturer(manufacturer);
            return Ok("Manufacturer added successfully.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("Delete")]
        public IActionResult DeleteManufacturer(string id)
        {
            _manufacturerService.DeleteManufacturer(id);
            return Ok("Manufacturer deleted successfully.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Update")]
        public IActionResult UpdateManufacturer(Manufacturer manufacturer)
        {
            _manufacturerService.UpdateManufacturer(manufacturer);
            return Ok("Manufacturer updated successfully.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllManufacturers")]
        public IActionResult GetAllManufacturers()
        {
            var manufacturers = _manufacturerService.GetAllManufacturers();
            return Ok(manufacturers);
        }

    }
}
