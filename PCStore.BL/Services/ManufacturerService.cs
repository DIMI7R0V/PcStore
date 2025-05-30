using Microsoft.Extensions.Logging;
using PCStore.BL.Interfaces;
using PCStore.DL.Interfaces;
using PCStore.DL.Repositories;
using PCStore.Models.Models;

namespace PCStore.BL.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly ILogger<ManufacturerService> _logger;

        public ManufacturerService(IManufacturerRepository manufacturerRepository, ILogger<ManufacturerService> logger)
        {
            _manufacturerRepository = manufacturerRepository;
            _logger = logger;
        }

        public async Task AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                if (manufacturer == null || string.IsNullOrWhiteSpace(manufacturer.Name))
                {
                    _logger.LogError("Invalid manufacturer data provided.");
                    return;
                }

                await _manufacturerRepository.AddManufacturer(manufacturer);
                _logger.LogInformation("Manufacturer {ManufacturerName} added successfully.", manufacturer.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding manufacturer.");
            }
        }

        public async Task<bool> DeleteManufacturer(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogError("Invalid manufacturer ID for deletion.");
                    return false;
                }

                var manufacturer = await _manufacturerRepository.GetManufacturer(id);
                if (manufacturer == null)
                {
                    _logger.LogWarning("Manufacturer with ID {ManufacturerId} not found for deletion.", id);
                    return false;
                }

                await _manufacturerRepository.DeleteManufacturer(id);
                _logger.LogInformation("Manufacturer with ID {ManufacturerId} deleted successfully.", id);
                return true; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting manufacturer.");
                return false; 
            }
        }

        public async Task<bool> UpdateManufacturer(Manufacturer manufacturer)
        {
            try
            {
                if (manufacturer == null || string.IsNullOrWhiteSpace(manufacturer.Id))
                {
                    _logger.LogError("Invalid manufacturer data for update.");
                    return false; 
                }

                var existingManufacturer = await _manufacturerRepository.GetManufacturer(manufacturer.Id);
                if (existingManufacturer == null)
                {
                    _logger.LogWarning("Manufacturer with ID {ManufacturerId} not found for update.", manufacturer.Id);
                    return false; 
                }

                await _manufacturerRepository.UpdateManufacturer(manufacturer);
                _logger.LogInformation("Manufacturer {ManufacturerName} updated successfully.", manufacturer.Name);
                return true; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating manufacturer.");
                return false; 
            }
        }

        public async Task<Manufacturer?> GetManufacturer(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogError("Invalid manufacturer ID provided.");
                    return null;
                }

                var manufacturer = await _manufacturerRepository.GetManufacturer(id);
                if (manufacturer == null)
                {
                    _logger.LogWarning("Manufacturer with ID {ManufacturerId} not found.", id);
                }

                return manufacturer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving manufacturer.");
                return null;
            }
        }

        public async Task<List<Manufacturer>> GetAllManufacturers()
        {
            try
            {
                var manufacturers = await _manufacturerRepository.GetAllManufacturers();
                if (manufacturers != null || manufacturers.Count != 0)
                {
                    return manufacturers;
                }
                _logger.LogWarning("No manufacturers found.");

                return manufacturers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving manufacturers.");
                return new List<Manufacturer>();
            }
        }
    }
}
