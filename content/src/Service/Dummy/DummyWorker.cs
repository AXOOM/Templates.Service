using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MyVendor.MyService.Dummy
{
    public class DummyWorker : WorkerBase
    {
        private readonly IDummyMetrics _metrics;
        private readonly ILogger<DummyWorker> _logger;

        public DummyWorker(IServiceScopeFactory scopeFactory, IDummyMetrics metrics, ILogger<DummyWorker> logger)
            : base(scopeFactory)
        {
            _metrics = metrics;
            _logger = logger;
        }

        protected override async Task DoWorkAsync(IServiceProvider provider, CancellationToken cancellationToken)
        {
            using (_metrics.Run())
            {
                _logger.LogDebug("Working");
                // TODO: To work
            }

            var sleep = provider.GetRequiredService<IOptions<DummyOptions>>().Value.Sleep;
            _logger.LogDebug("Sleeping for {0}", sleep);
            await Task.Delay(sleep, cancellationToken);
        }
    }
}
