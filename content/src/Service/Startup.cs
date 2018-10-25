using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVendor.MyService.Dummy;
using MyVendor.MyService.Infrastructure;

namespace MyVendor.MyService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
            => services.AddInfrastructure(Configuration)
                       .AddDummy(Configuration.GetSection("Dummy"));
    }
}
