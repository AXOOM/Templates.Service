using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MyVendor.MyService.Dummy
{
    public class StartupFacts : StartupFactsBase
    {
        [Fact]
        public void CanResolveDummyWorker()
        {
            Provider.GetRequiredService<DummyWorker>();
        }
    }
}
