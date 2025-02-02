using PCStore.Models.Models;

namespace PCStore.BL.Interfaces
{
    public interface IManufacturerService
    {
        void AddManufacturer(Manufacturer manufacturer);

        bool DeleteManufacturer(string id);

        bool UpdateManufacturer(Manufacturer manufacturer);

        Manufacturer? GetManufacturer(string id);

        List<Manufacturer> GetAllManufacturers();
    }
}
