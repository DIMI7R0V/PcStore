using PCStore.Models.Models;

namespace PCStore.Models.Responces
{
    public class GetAllProductsFromManufacturerResponce
    {
        public Manufacturer? Manufacturer { get; set; }

        public List<Product>? Products { get; set; }
    }
}
