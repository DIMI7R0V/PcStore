using FluentValidation;
using PCStore.Validation;
using PcStoreBL;

namespace PcStore
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterDataLayer();
            services.RegisterBusinessLayer();
            services.AddValidatorsFromAssemblyContaining<GetAllProductsFromManufacturerRequestValidaror>();
        }
    }
}
