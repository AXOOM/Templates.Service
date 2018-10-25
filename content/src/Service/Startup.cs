using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVendor.MyService.Dummy;
using MyVendor.MyService.Infrastructure;

namespace MyVendor.MyService
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                           .AddYamlFile("appsettings.yml", optional: false, reloadOnChange: true)
                           .AddEnvironmentVariables()
                           .Build();
        }

        public void ConfigureServices(IServiceCollection services)
            => services.AddInfrastructure(Configuration)
                       .AddDummy(Configuration.GetSection("Dummy"));

        public void Configure(IServiceProvider provider)
        {
            provider.UseInfrastructure();
        }
    }
}
