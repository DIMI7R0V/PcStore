using Mapster;
using PCStore.Models.Models;
using PCStore.Models.Requests;


namespace PcStoreStore.MappsterConfig
{
    public class MappsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Product, GetAllProductsFromManufacturerRequest >
                .NewConfig()
                .TwoWays();
        }
    }
}
