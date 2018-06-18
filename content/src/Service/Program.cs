using Microsoft.Extensions.Hosting;

namespace MyVendor.MyService
{
    public static class Program
    {
        public static void Main()
        {
            var startup = new Startup();
            var builder = new HostBuilder().ConfigureServices(startup.ConfigureServices)
                                           .UseConsoleLifetime();

            using (var host = builder.Build())
            {
                host.Start();
                startup.Configure(host.Services);
                host.WaitForShutdown();
            }
        }
    }
}
