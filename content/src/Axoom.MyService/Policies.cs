using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using Axoom.Extensions.Logging.Extensions;
using Microsoft.Extensions.Logging;
using Polly;

namespace Axoom.MyService
{
    /// <summary>
    /// Provides error handling and retry policies.
    /// </summary>
    public static class Policies
    {
        /// <summary>
        /// Policy for handling connection problems with external services during startup.
        /// </summary>
        public static Task StartupAsync(ILogger logger, Func<Task> action) => Policy
            .Handle<SocketException>()
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(
                sleepDurations: new[] {TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(20)},
                onRetry: (ex, timeSpan) => logger.LogWarning(ex, $"Problem connecting to external service. Retrying in {timeSpan}."))
            .ExecuteAsync(action);
    }
}