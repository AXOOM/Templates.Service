using Microsoft.Extensions.DependencyInjection;

namespace MyVendor.MyService.Dummy
{
    public static class Startup
    {
        public static IServiceCollection AddDummy(this IServiceCollection services)
            => services.AddSingleton<DummyWorker>()
                       .AddSingleton<IDummyMetrics, DummyMetrics>();
    }
}
