using System;
using Axoom.Extensions.Logging.Console;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyVendor.MyService
{
    // Manage process lifecycle, configuration and logging
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            RunInitTasks(host);
            host.Run();
        }

        [PublicAPI]
        public static IHostBuilder CreateHostBuilder(string[] args)
            => new HostBuilder()
              .UseConsoleLifetime()
              .ConfigureHostConfiguration(builder => builder.AddEnvironmentVariables())
              .ConfigureAppConfiguration((context, builder) =>
               {
                   var env = context.HostingEnvironment;
                   builder.SetBasePath(env.ContentRootPath)
                          .AddYamlFile("appsettings.yml", optional: false, reloadOnChange: true)
                          .AddYamlFile($"appsettings.{env.EnvironmentName}.yml", optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables()
                          .AddUserSecrets<Startup>()
                          .AddCommandLine(args);
               })
              .ConfigureLogging((context, builder) =>
               {
                   var config = context.Configuration.GetSection("Logging");
                   builder.AddConfiguration(config)
                          .AddAxoomConsole(config)
                          .AddExceptionDemystifyer();
               })
              .ConfigureServices((context, services) => new Startup(context.Configuration).ConfigureServices(services));

        private static void RunInitTasks(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                try
                {
                    Startup.Init(provider);
                }
                catch (Exception ex)
                {
                    provider.GetRequiredService<ILogger<Startup>>().LogCritical(ex, "Startup.Init() failed.");
                    throw;
                }
            }
        }
    }
}
