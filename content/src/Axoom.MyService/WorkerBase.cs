using System;
using System.Threading;
using System.Threading.Tasks;
using Axoom.MyService.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axoom.MyService
{
    /// <summary>
    /// Common base class for jobs that run in a continous loop.
    /// </summary>
    public abstract class WorkerBase
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        protected WorkerBase(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

        /// <summary>
        /// Starts running the worker. Use <see cref="Dispose"/> to stop it.
        /// </summary>
        public async void Start() // NOTE: Using async void here only because all exceptions are caught and handled
        {
            try
            {
                using (new CancellationGuard(_cts.Token))
                {
                    while (!_cts.IsCancellationRequested)
                    {
                        using (var scope = _scopeFactory.CreateScope())
                            await DoWorkAsync(scope.ServiceProvider, _cts.Token);
                    }
                }
            }
            catch (OperationCanceledException) {}
            catch (Exception ex)
            {
                using (var scope = _scopeFactory.CreateScope())
                    scope.ServiceProvider.GetRequiredService<ILogger<WorkerBase>>().LogCritical(ex, "Unhandled exception.");
                Environment.Exit(exitCode: 1);
            }
        }

        /// <summary>
        /// Template method that is called in a loop to perform the actual work. Remember to sleep as appropriate.
        /// </summary>
        /// <param name="provider">Scoped service provider. Will be disposed and recreated for each run.</param>
        /// <param name="cancellationToken">Cancellation token used to indicate that the worker was <see cref="Dispose"/>d.</param>
        protected abstract Task DoWorkAsync(IServiceProvider provider, CancellationToken cancellationToken);

        /// <summary>
        /// Stops the worker.
        /// </summary>
        public void Dispose() => _cts.Cancel();
    }
}
