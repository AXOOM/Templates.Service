using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axoom.MyService.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services
            .AddOptions()
            .AddSingleton(configuration)
            .AddAxoomLogging(configuration)
            .AddPolicies(configuration.GetSection("Policies"))
            .AddMetrics();

        public static IServiceProvider UseInfrastructure(this IServiceProvider provider)
        {
            provider.UseAxoomLogging();
            provider.ExposeMetrics(port: 5000);

            return provider;
        }
    }
}
