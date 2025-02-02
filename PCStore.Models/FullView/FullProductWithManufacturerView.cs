using PCStore.Models.Models;

namespace PCStore.Models.FullView
{
    public class FullProductWithManufacturerView
    {
        public string ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public List<Product> Products { get; set; } 
    }
}
