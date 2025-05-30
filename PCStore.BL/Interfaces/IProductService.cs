using PCStore.Models.Models;

namespace PCStore.BL.Interfaces
{
    public interface IProductService
    {
        Task AddProduct(Product product);

        Task<bool> DeleteProduct(string id);

        Task<bool> UpdateProduct(Product product);

        Task<Product?> GetProduct(string id);

        Task<List<Product>> GetAllProductsByManufacturer(string manufacturerId);

        Task<List<Product>> GetAllProducts();
    }
}
