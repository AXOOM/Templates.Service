using System;
using Axoom.Extensions.Configuration.Yaml;
using Axoom.Extensions.Logging.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Startup.Core;

namespace Axoom.MyService
{
    public class Startup : IStartup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup() => Configuration = new ConfigurationBuilder()
            .AddYamlFile("appsettings.yml", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public IServiceProvider ConfigureServices(IServiceCollection services) => services
            .AddLogging(builder => builder.AddConfiguration(Configuration.GetSection("Logging")))
            .AddOptions()
            .AddPolicies(Configuration.GetSection("Policies"))
            .AddMetrics()
            //.Configure<MyOptions>(Configuration.GetSection("MyOptions"))
            //.AddTransient<IMyService, MyService>()
            //.AddSingleton<Worker>()
            .BuildServiceProvider();

        public void Configure(IServiceProvider provider)
        {
            provider
                .GetRequiredService<ILoggerFactory>()
                .AddAxoomConsole(Configuration.GetSection("Logging"))
                .CreateLogger<Startup>()
                .LogInformation("Starting My Service");

            //provider.GetRequiredService<IPolicies>().Startup(async () =>
            //{
            //    await provider.GetRequiredService<Worker>().StartAsync();
            //});

            provider.ExposeMetrics(port: 5000);
        }
    }
}