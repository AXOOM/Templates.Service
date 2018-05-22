using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVendor.MyService.Dummy;
using MyVendor.MyService.Infrastructure;
using Startup.Core;

namespace MyVendor.MyService
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

        /// <inheritdoc />
        public IServiceProvider ConfigureServices(IServiceCollection services)
            => services.AddInfrastructure(Configuration)
                       .AddDummy()
                       .BuildServiceProvider();

        /// <inheritdoc />
        public void Configure(IServiceProvider provider)
        {
            provider.UseInfrastructure();
            provider.GetRequiredService<Policies>().Startup(() =>
            {
                using (var scope = provider.CreateScope())
                {
                    // TODO: Connect to external services
                }
            });
            provider.GetRequiredService<DummyWorker>().Start();
        }
    }
}
