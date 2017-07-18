using System;
using Axoom.Extensions.Configuration.Yaml;
using Axoom.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging()
                .AddOptions()
                .AddPolicies(Configuration.GetSection("Policies"))
                //.Configure<MyOptions>(Configuration.GetSection("MyOptions"))
                //.AddTransient<IMyService, MyService>()
                //.AddSingleton<Worker>()
                ;
        }

        public void Configure(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            loggerFactory.AddAxoomLogging(Configuration.GetSection("Logging"));
            var logger = loggerFactory.CreateLogger<Startup>();
            logger.LogInformation("Starting My Service");

            //provider.GetService<IPolicies>().StartupAsync(async () =>
            //{
            //    await provider.GetService<Worker>().StartAsync();
            //}).Wait();
        }
    }
}