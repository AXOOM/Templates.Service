using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace MyVendor.MyService
{
    public class StartupFacts
    {
        private readonly ITestOutputHelper _output;

        private readonly IConfiguration _configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
        {
        }).Build();

        private readonly ServiceCollection _services = new ServiceCollection();
        private readonly IServiceProvider _provider;

        public StartupFacts(ITestOutputHelper output)
        {
            _output = output;

            _services.AddLogging(builder => builder.AddXUnit(output));
            new Startup(_configuration).ConfigureServices(_services);

            _provider = _services.BuildServiceProvider();
        }

        [Fact]
        public void CanResolveAllRegisteredServices()
        {
            foreach (var type in _services.Select(x => x.ServiceType).Where(x => !x.IsGenericTypeDefinition))
            {
                _output.WriteLine("Resolving {0}", type);
                _provider.GetRequiredService(type);
            }
        }
    }
}
