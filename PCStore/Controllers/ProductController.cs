using PCStore.BL.Interfaces;
using PCStore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using PCStore.BL.Services;

namespace PCStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _productService.GetProduct(id);
            if (product == null) return NotFound("Product not found.");
            return Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            
            await _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _productService.DeleteProduct(id);

            if (result)
                return NoContent();

            return NotFound($"Product with ID {id} not found.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var result = await _productService.UpdateProduct(product);
            if (result)
                return NoContent();
            return NotFound($"Product with ID {product.Id} not found.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

    }
}
