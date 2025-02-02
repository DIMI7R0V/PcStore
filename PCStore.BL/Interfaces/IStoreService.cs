using PCStore.Models.FullView;
using PCStore.Models.Requests;
using PCStore.Models.Responces;

namespace PCStore.BL.Interfaces
{
    public interface IStoreService
    {
        GetAllProductsFromManufacturerResponce
            GetAllProductsByManufacturer(GetAllProductsFromManufacturerRequest request);

        int CheckProductCount(int input);

        List<FullProductWithManufacturerView> GetFullInformation();
    }
}
