using System;
using System.Threading;
using Microsoft.Extensions.Options;
using Xunit;

namespace MyVendor.MyService.Dummy
{
    public class DummyFacts : AutoMockingFactsBase<DummyWorker>
    {
        public DummyFacts()
        {
            Use(Options.Create(new DummyOptions {Sleep = TimeSpan.FromSeconds(10)}));
        }

        [Fact]
        public void UpdatesMetrics()
        {
            Subject.StartAsync(default);
            Subject.Dispose();

            GetMock<IDummyMetrics>().Verify(x => x.Run());
        }
    }
}
