using PCStore.Models.FullView;
using PCStore.Models.Requests;
using PCStore.Models.Responces;

namespace PCStore.BL.Interfaces
{
    public interface IStoreService
    {
        Task<GetAllProductsFromManufacturerResponce>
            GetAllProductsByManufacturer(GetAllProductsFromManufacturerRequest request);

        Task<int> CheckProductCount(int input);

        Task<List<FullProductWithManufacturerView>> GetFullInformation();
    }
}
