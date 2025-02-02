using PCStore.Models.Models;

namespace PCStore.BL.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);

        bool DeleteProduct(string id);

        bool UpdateProduct(Product product);

        Product? GetProduct(string id);

        List<Product> GetAllProductsByManufacturer(string manufacturerId);

        List<Product> GetAllProducts();
    }
}
