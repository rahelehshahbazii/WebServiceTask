using Microsoft.Extensions.DependencyInjection;
using WebAPIService.Domain.Interfaces;
using WebAPIService.Services;

namespace WebAPIService.Dependencies
{
    public static class ServicesDependency
    {

        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddTransient<IServiceFile, ServiceFile>();
        }
    }
}
