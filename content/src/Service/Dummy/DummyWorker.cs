using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axoom.MyService.Dummy
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

            _logger.LogDebug("Sleeping for 10s");
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }
    }
}
