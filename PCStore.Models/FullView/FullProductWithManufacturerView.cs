using PCStore.Models.Models;

namespace PCStore.Models.FullView
{
    public class FullProductWithManufacturerView
    {
        public string ManufacturerId { get; set; } = string.Empty;

        public string ManufacturerName { get; set; } = string.Empty;

        public List<Product> Products { get; set; }
    }
}
