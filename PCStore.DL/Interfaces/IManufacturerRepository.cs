using PCStore.Models.Models;

namespace PCStore.DL.Interfaces
{
    public interface IManufacturerRepository
    {
        Task AddManufacturer(Manufacturer manufacturer);

        Task<bool> DeleteManufacturer(string id);

        Task<bool> UpdateManufacturer(Manufacturer manufacturer);

        Task<Manufacturer?> GetManufacturer(string id);

        Task<List<Manufacturer>> GetAllManufacturers();
    }
}
