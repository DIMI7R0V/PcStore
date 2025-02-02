using Microsoft.Extensions.DependencyInjection;
using PCStore.DL.Interfaces;
using PCStore.DL.Repositories;

namespace PcStoreDL
{
    public static class DependencyInjection
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services
                .AddSingleton<IManufacturerRepository, ManufacturerRepository>()
                .AddSingleton<IProductRepository, ProductRepository>();
        }
    }
}
