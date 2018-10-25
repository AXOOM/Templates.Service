using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MyVendor.MyService
{
    public static class Program
    {
        public static void Main()
            => new HostBuilder().UseConsoleLifetime()
                                .ConfigureHostConfiguration(builder => builder.AddEnvironmentVariables())
                                .ConfigureAppConfiguration(Configuration)
                                .ConfigureServices((context, services) => new Startup(context.Configuration).ConfigureServices(services))
                                .Build()
                                .Run();

        private static void Configuration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            var env = context.HostingEnvironment;
            builder.SetBasePath(env.ContentRootPath)
                   .AddYamlFile("appsettings.yml", optional: false, reloadOnChange: true)
                   .AddYamlFile($"appsettings.{env.EnvironmentName}.yml", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();
        }
    }
}
