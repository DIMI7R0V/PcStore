using PCStore.Models.Models;

namespace PCStore.DL.Interfaces
{
    public interface IManufacturerRepository
    {
        void AddManufacturer(Manufacturer manufacturer);

        bool DeleteManufacturer(string id); 

        bool UpdateManufacturer(Manufacturer manufacturer);

        Manufacturer? GetManufacturer(string id); 

        List<Manufacturer> GetAllManufacturers();
    }
}
