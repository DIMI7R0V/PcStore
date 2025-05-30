using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PcStore.Models.Configurations;
using PCStore.DL.Interfaces;
using PCStore.Models.Models;

namespace PCStore.DL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(IOptionsMonitor<MongoDBConfiguration> mongoConfig, ILogger<ProductRepository> logger)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task AddProduct(Product product)
        {
            if (product == null)
            {
                _logger.LogError("Invalid product");
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            product.Id = Guid.NewGuid().ToString();
            await _products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid product ID you want to delete!");
                return false;
            }

            var result = await _products.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<Product?> GetProduct(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid product ID!");
                return null;
            }

            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _products.Find(p => true).ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsByManufacturer(string manufacturerId)
        {
            if (string.IsNullOrWhiteSpace(manufacturerId))
            {
                _logger.LogError("Invalid manufacturer ID!");
                return new List<Product>();
            }

            return await _products.Find(p => p.ManufacturerId == manufacturerId).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.Id))
            {
                _logger.LogError("Invalid product you want to update.");
                return false;
            }

            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var updateResult = await _products.ReplaceOneAsync(filter, product);

            return updateResult.ModifiedCount > 0;
        }
    }
}
