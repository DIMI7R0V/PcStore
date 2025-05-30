using PCStore.DL.Interfaces;
using MongoDB.Driver;
using PCStore.Models.Models;
using Microsoft.Extensions.Logging;
using PcStore.Models.Configurations;
using Microsoft.Extensions.Options;


namespace PCStore.DL.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IMongoCollection<Manufacturer> _manufacturers;
        private readonly ILogger<ManufacturerRepository> _logger;

        public ManufacturerRepository(IOptionsMonitor<MongoDBConfiguration> mongoConfig, ILogger<ManufacturerRepository> logger)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            _manufacturers = database.GetCollection<Manufacturer>($"{nameof(Manufacturer)}s");
        }

        public async Task AddManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                _logger.LogError("Invalid manufacturer data provided for insertion.");
                throw new ArgumentNullException(nameof(manufacturer), "Manufacturer cannot be null.");
            }

            manufacturer.Id = Guid.NewGuid().ToString();
            await _manufacturers.InsertOneAsync(manufacturer);
        }

        public async Task<bool> DeleteManufacturer(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid manufacturer ID!");
                return false;
            }

            var result = await _manufacturers.DeleteOneAsync(m => m.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<Manufacturer?> GetManufacturer(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid manufacturer ID!");
                return null;
            }

            return await _manufacturers.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Manufacturer>> GetAllManufacturers()
        {
            return await _manufacturers.Find(m => true).ToListAsync();
        }

        public async Task<bool> UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null || string.IsNullOrWhiteSpace(manufacturer.Id))
            {
                _logger.LogError("Invalid manufacturer you want to update.");
                return false;
            }

            var filter = Builders<Manufacturer>.Filter.Eq(m => m.Id, manufacturer.Id);
            var updateResult = await _manufacturers.ReplaceOneAsync(filter, manufacturer);

            return updateResult.ModifiedCount > 0;
        }
    }
}
