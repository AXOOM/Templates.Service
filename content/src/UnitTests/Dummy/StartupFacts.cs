using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace MyVendor.MyService.Dummy
{
    public class StartupFacts : StartupFactsBase
    {
        [Fact]
        public void CanResolveDummyWorker()
        {
            Provider.GetServices<IHostedService>().OfType<DummyWorker>().Should().NotBeEmpty();
        }
    }
}
