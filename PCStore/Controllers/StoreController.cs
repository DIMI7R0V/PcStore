using Microsoft.AspNetCore.Mvc;
using PCStore.BL.Interfaces;
using PCStore.Models.Models;
using PCStore.Models.Requests;
using PCStore.Models.Responces;

namespace PCStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<StoreController> _logger;
        public StoreController(IStoreService StoreService, ILogger<StoreController> logger)
        {
            _storeService = StoreService;
            _logger = logger;
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetFullProductView")]
        public async Task<IActionResult> GetFullProductView()
        {

            var result = await _storeService.GetFullInformation();

            if (result == null || !result.Any())
            {
                return NotFound("No products found!");
            }

            return Ok(result);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("GetAllProductsFromManufacturer")]
        public async Task<IActionResult> GetAllProductsFromManufacturer([FromBody] GetAllProductsFromManufacturerRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ManufacturerId))
            {
                _logger.LogWarning("ManufacturerId cannot be null or empty!");
                return BadRequest("ManufacturerId cannot be null or empty!");
            }

            var result = await _storeService.GetAllProductsByManufacturer(request);

            if (result == null || result.Products == null || !result.Products.Any())
            {
                _logger.LogWarning("Products from this manufacturer Not Found!");
                return NotFound("Products from this manufacturer Not Found!");
            }

            _logger.LogInformation("Products from this manufacturer retrieved.");
            return Ok(result);
        }
    }
}
