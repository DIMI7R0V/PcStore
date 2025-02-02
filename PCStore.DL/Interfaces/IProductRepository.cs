using PCStore.Models.Models;

namespace PCStore.DL.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);

        bool DeleteProduct(string id);

        bool UpdateProduct(Product product);

        Product? GetProduct(string id);

        List<Product> GetAllProducts();

        List<Product> GetAllProductsByManufacturer(string manufacturerId);
    }
}
