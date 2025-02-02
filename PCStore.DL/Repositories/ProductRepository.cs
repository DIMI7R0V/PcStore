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
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                _logger.LogError("Invalid product");
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            product.Id = Guid.NewGuid().ToString();
            _products.InsertOne(product);
        }

        public bool DeleteProduct(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid product ID you want to delete!");
                return false;
            }

            var result = _products.DeleteOne(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public Product? GetProduct(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid product ID!");
                return null;
            }

            return _products.Find(p => p.Id == id).FirstOrDefault();
        }

        public List<Product> GetAllProducts()
        {
            return _products.Find(p => true).ToList();
        }

        public List<Product> GetAllProductsByManufacturer(string manufacturerId)
        {
            if (string.IsNullOrWhiteSpace(manufacturerId))
            {
                _logger.LogError("Invalid manufacturer ID!");
                return new List<Product>();
            }

            return _products.Find(p => p.ManufacturerId == manufacturerId).ToList();
        }

        public bool UpdateProduct(Product product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.Id))
            {
                _logger.LogError("Invalid product you want to update.");
                return false;
            }

            var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var updateResult = _products.ReplaceOne(filter, product);

            return updateResult.ModifiedCount > 0;
        }
    }
}
