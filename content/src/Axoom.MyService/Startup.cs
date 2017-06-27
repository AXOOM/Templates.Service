using System;
using Axoom.Extensions.Configuration.Yaml;
using Axoom.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Startup.Core;

namespace Axoom.MyService
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup : IStartup
    {
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Called to set up an environment.
        /// </summary>
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .AddYamlFile("appsettings.yml", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Called to register services.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging()
                .AddOptions()
                //.Configure<MyOptions>(Configuration.GetSection("MyOptions"))
                //.AddSingleton<IMyRemoteService, MyRemoteService>()
                ;
        }

        /// <summary>
        /// Called to configure services after they have been registered.
        /// </summary>
        public void Configure(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            loggerFactory.AddAxoomLogging("Axoom.MyService");
        }
    }
}