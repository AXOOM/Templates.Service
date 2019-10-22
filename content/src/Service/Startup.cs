using Axoom.Extensions.Prometheus.Standalone;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVendor.MyService.Dummy;

namespace MyVendor.MyService
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Register services for DI
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPrometheusServer(_configuration.GetSection("Metrics"));

            services.AddDummy(_configuration.GetSection("Dummy"));
        }
    }
}
