using System;
using JetBrains.Annotations;
using Nexogen.Libraries.Metrics;
using Nexogen.Libraries.Metrics.Extensions;

namespace MyVendor.MyService.Dummy
{
    /// <summary>
    /// Used to report metrics relating to the dummy worker.
    /// </summary>
    [UsedImplicitly]
    public class DummyMetrics : IDummyMetrics
    {
        private readonly ICounter _runs;
        private readonly IHistogram _runDurations;

        public DummyMetrics(IMetrics metrics)
        {
            _runs = metrics.Counter()
                           .Name("myservice_dummy_runs")
                           .Help("Number of times the dummy worker ran")
                           .Register();
            _runDurations = metrics.Histogram()
                                   .Name("myservice_dummy_duration_seconds")
                                   .Help("Average duration of a dummy worker run")
                                   .Register();
        }

        /// <inheritdoc/>
        public IDisposable Run()
        {
            _runs.Increment();
            return _runDurations.Timer();
        }
    }
}
