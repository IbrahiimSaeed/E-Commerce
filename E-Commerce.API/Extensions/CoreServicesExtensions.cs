using Services;
using Services.Abstraction.Contracts;
using Services.Implementations;

namespace E_Commerce.API.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);
            return services;
        }
    }
}
