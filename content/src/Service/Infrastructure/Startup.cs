using System;
using Axoom.Extensions.Prometheus.Standalone;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyVendor.MyService.Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
            => services.AddSingleton(configuration)
                       .AddOptions()
                       .AddAxoomLogging(configuration)
                       .AddPrometheusServer(configuration)
                       .AddPolicies(configuration);

        public static IServiceProvider UseInfrastructure(this IServiceProvider provider)
        {
            provider.UseAxoomLogging();
            provider.UsePrometheusServer();

            return provider;
        }
    }
}
