using Axoom.Extensions.Prometheus.Standalone;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVendor.MyService.Dummy;

namespace MyVendor.MyService
{
    /// <summary>
    /// Configures dependency injection.
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPrometheusServer(Configuration.GetSection("Metrics"));

            services.AddDummy(Configuration.GetSection("Dummy"));
        }
    }
}
