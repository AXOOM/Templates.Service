using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace MyVendor.MyService.Dummy
{
    public class StartupFacts : StartupFactsBase
    {
        public StartupFacts(ITestOutputHelper output)
            : base(output)
        {}

        [Fact]
        public void CanResolveDummyWorker()
        {
            Provider.GetServices<IHostedService>().OfType<DummyWorker>().Should().NotBeEmpty();
        }
    }
}
