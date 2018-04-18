using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Axoom.MyService.Dummy
{
    public class DummyFacts
    {
        private readonly Mock<IDummyMetrics> _metricsMock = new Mock<IDummyMetrics>();

        [Fact]
        public void NotesRunsInMetrics()
        {
            var provider = new ServiceCollection().BuildServiceProvider();
            var worker = new DummyWorker(
                provider.GetRequiredService<IServiceScopeFactory>(),
                _metricsMock.Object,
                new Mock<ILogger<DummyWorker>>().Object);

            worker.Start();
            worker.Dispose();

            _metricsMock.Verify(x => x.Run());
        }
    }
}
