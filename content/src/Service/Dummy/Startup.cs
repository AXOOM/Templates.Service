using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyVendor.MyService.Dummy
{
    public static class Startup
    {
        public static IServiceCollection AddDummy(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<DummyOptions>(configuration)
                       .AddSingleton<IDummyMetrics, DummyMetrics>()
                       .AddHostedService<DummyWorker>();
    }
}
