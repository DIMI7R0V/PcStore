using PcStoreDL;
using PCStore.BL.Interfaces;
using PCStore.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PcStoreBL
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterBusinessLayer(this IServiceCollection services)
        {
            services.AddSingleton<IStoreService, StoreService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IManufacturerService, ManufacturerService>();

            return services;
        }

        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            services.RegisterRepositories();

            return services;
        }
    }
}
