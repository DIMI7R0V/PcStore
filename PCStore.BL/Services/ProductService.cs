using Microsoft.Extensions.Logging;
using PCStore.BL.Interfaces;
using PCStore.DL.Interfaces;
using PCStore.Models.Models;
using PCStore.Models.Requests;

namespace PCStore.BL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                _logger.LogError("Product cannot be null!");
                throw new ArgumentNullException(nameof(product), "Product cannot be null!");
            }

            _logger.LogInformation("Adding product!");

            try
            {
                _productRepository.AddProduct(product);
                _logger.LogInformation("Product successfully added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding product");
                throw;
            }
        }

        public bool DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Invalid product ID you want to delete.");
                return false;
            }

            _logger.LogInformation($"Deleting product with ID: {id}");

            try
            {
                var product = _productRepository.GetProduct(id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID: {id} not found.");
                    return false;
                }

                _productRepository.DeleteProduct(id);
                _logger.LogInformation($"Product with ID: {id} deleted successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting product with ID: {id}");
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            _logger.LogInformation("Retrieving all products.");
            try
            {
                var products = _productRepository.GetAllProducts();
                _logger.LogInformation("Retrieved {ProductCount} products for this manufacturer", products.Count);
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all products.");
                throw;
            }
        }

        public List<Product> GetAllProductsByManufacturer(string manufacturerId)
        {
            if (string.IsNullOrEmpty(manufacturerId))
            {
                _logger.LogWarning("Invalid manufacturer ID provided.");
                throw new ArgumentException("Invalid manufacturer ID.", nameof(manufacturerId));
            }

            _logger.LogInformation("Retrieving products for manufacturer");
            try
            {
                var products = _productRepository.GetAllProductsByManufacturer(manufacturerId);
                _logger.LogInformation("Retrieved products for manufacturer");
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving products for manufacturer");
                throw;
            }
        }

        public Product? GetProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Invalid product ID provided.");
                throw new ArgumentException("Invalid product ID.", nameof(id));
            }

            _logger.LogInformation("Retrieving product");
            try
            {
                var product = _productRepository.GetProduct(id);
                if (product == null)
                {
                    _logger.LogWarning("No product found");
                }
                else
                {
                    _logger.LogInformation("Retrieved product");
                }
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product");
                throw;
            }
        }

        public bool UpdateProduct(Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.Id))
            {
                _logger.LogWarning("Invalid product provided for update.");
                return false;
            }

            _logger.LogInformation("Updating product");

            try
            {
                var existingProduct = _productRepository.GetProduct(product.Id);
                if (existingProduct == null)
                {
                    _logger.LogWarning("Product not found for update.");
                    return false;
                }

                _productRepository.UpdateProduct(product);
                _logger.LogInformation("Product successfully updated.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product ID:");
                return false;
            }
        }
    }
}
