using System;

namespace Axoom.MyService.Dummy
{
    /// <summary>
    /// Used to report metrics relating to the dummy worker.
    /// </summary>
    public interface IDummyMetrics
    {
        /// <summary>
        /// Counts one run of a worker. Starts a timer for the duration of the run. Place inside a using statement.
        /// </summary>
        IDisposable Run();
    }
}
