using System;
using Axoom.Extensions.Configuration.Yaml;
using Axoom.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nexogen.Libraries.Metrics.Prometheus;
using Nexogen.Libraries.Metrics.Prometheus.Standalone;
using Startup.Core;

namespace Axoom.MyService
{
    public class Startup : IStartup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .AddYamlFile("appsettings.yml", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services) => services
            .AddLogging(builder => builder.AddConfiguration(Configuration.GetSection("Logging")))
            .AddSingleton<PrometheusMetrics>()
            .AddOptions()
            .AddPolicies(Configuration.GetSection("Policies"))
            //.Configure<MyOptions>(Configuration.GetSection("MyOptions"))
            //.AddTransient<IMyService, MyService>()
            //.AddSingleton<Worker>()
            ;

        public void Configure(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            loggerFactory
                .AddAxoomConsole(Configuration.GetSection("Logging"))
                .CreateLogger<Startup>()
                .LogInformation("Starting My Service");

            provider.GetRequiredService<PrometheusMetrics>().Server().Port(5000).Start();

            //provider.GetRequiredService<IPolicies>().StartupAsync(async () =>
            //{
            //    await provider.GetRequiredService<Worker>().StartAsync();
            //}).Wait();
        }
    }
}