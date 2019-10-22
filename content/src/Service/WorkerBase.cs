using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyVendor.MyService
{
    /// <summary>
    /// Common base class for jobs that run in a continuous loop.
    /// </summary>
    public abstract class WorkerBase : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        protected WorkerBase(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (new CancellationGuard(stoppingToken))
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        using var scope = _scopeFactory.CreateScope();
                        await DoWorkAsync(scope.ServiceProvider, stoppingToken);
                    }
                }
            }
            catch (OperationCanceledException) {}
        }

        /// <summary>
        /// Template method that is called in a loop to perform the actual work. Remember to sleep as appropriate.
        /// </summary>
        /// <param name="provider">Scoped service provider. Will be disposed and recreated for each run.</param>
        /// <param name="cancellationToken">Cancellation token used to indicate that the worker was stopped.</param>
        protected abstract Task DoWorkAsync(IServiceProvider provider, CancellationToken cancellationToken);
    }
}
