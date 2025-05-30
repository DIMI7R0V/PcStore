using PCStore.Models.Models;

namespace PCStore.BL.Interfaces
{
    public interface IManufacturerService
    {
        Task AddManufacturer(Manufacturer manufacturer);

        Task<bool> DeleteManufacturer(string id);

        Task<bool> UpdateManufacturer(Manufacturer manufacturer);

        Task<Manufacturer?> GetManufacturer(string id);

        Task<List<Manufacturer>> GetAllManufacturers();
    }
}
