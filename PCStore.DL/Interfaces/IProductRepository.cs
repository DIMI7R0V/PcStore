using PCStore.Models.Models;

namespace PCStore.DL.Interfaces
{
    public interface IProductRepository
    {
        Task AddProduct(Product product);

        Task<bool> DeleteProduct(string id);

        Task<bool> UpdateProduct(Product product);

        Task<Product?> GetProduct(string id);

        Task<List<Product>> GetAllProducts();

        Task<List<Product>> GetAllProductsByManufacturer(string manufacturerId);
    }
}
