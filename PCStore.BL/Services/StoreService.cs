using PCStore.BL.Interfaces;
using PCStore.Models.Responces;
using PCStore.Models.Requests;
using Microsoft.Extensions.Logging;
using PCStore.Models.FullView;

namespace PCStore.BL.Services
{
    public class StoreService : IStoreService
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        private readonly ILogger<StoreService> _logger;

        public StoreService(IManufacturerService manufacturerService, IProductService productService, ILogger<StoreService> logger)
        {
            _manufacturerService = manufacturerService;
            _productService = productService;
            _logger = logger;
        }

        public async Task<int> CheckProductCount(int input)
        {
            if (input < 0)
            {
                _logger.LogWarning("Negative input value provided for product count!");
                return 0;
            }

            var productCount = await _productService.GetAllProducts();
            var totalCount = productCount.Count + input;

            return totalCount;
        }

        public async Task<GetAllProductsFromManufacturerResponce> GetAllProductsByManufacturer(GetAllProductsFromManufacturerRequest request)
        {
            var response = new GetAllProductsFromManufacturerResponce();

            if (string.IsNullOrWhiteSpace(request?.ManufacturerId))
            {
                _logger.LogWarning("Invalid manufacturer ID provided!");
                return response;
            }

            _logger.LogInformation("Retrieving manufacturer...");
            var manufacturer = await _manufacturerService.GetManufacturer(request.ManufacturerId);

            if (manufacturer == null)
            {
                _logger.LogWarning("Manufacturer with ID {ManufacturerId} not found.", request.ManufacturerId);
                return response;
            }

            response.Manufacturer = manufacturer;
            _logger.LogInformation("Manufacturer found.");

            _logger.LogInformation("Retrieving products for manufacturer...");
            var products = await _productService.GetAllProductsByManufacturer(request.ManufacturerId);

            if (products == null || products.Count == 0)
            {
                _logger.LogWarning("No products found for manufacturer with ID {ManufacturerId}.", request.ManufacturerId);
            }
            else
            {
                response.Products = products;
                _logger.LogInformation("Retrieved {Count} products for manufacturer.", products.Count);
            }

            return response;
        }

        public async Task<List<FullProductWithManufacturerView>> GetFullInformation()
        {
            var result = new List<FullProductWithManufacturerView>();

            var manufacturers = await _manufacturerService.GetAllManufacturers();

            if (manufacturers == null || !manufacturers.Any())
            {
                _logger.LogWarning("No manufacturers found.");
                return result;
            }

            foreach (var manufacturer in manufacturers)
            {
                var products = await _productService.GetAllProductsByManufacturer(manufacturer.Id);

                var manufacturerView = new FullProductWithManufacturerView
                {
                    ManufacturerId = manufacturer.Id,
                    ManufacturerName = manufacturer.Name,
                    Products = products
                };

                result.Add(manufacturerView);
            }

            _logger.LogInformation("Successfully retrieved {Count} manufacturers with products.", result.Count);
            return result;
        }

    }
}
