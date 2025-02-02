using PcStore.Models.Configurations;

namespace PcStore.ServiceExtentions
{
    public static class ServiceConfigurationsExtensions
    {
        public static IServiceCollection AddConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<MongoDBConfiguration>(
                configuration.GetSection(nameof(MongoDBConfiguration)));
        }
    }
}
