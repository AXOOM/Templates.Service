using System;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Axoom.MyService
{
    public static class Program
    {
        public static void Main()
        {
            var startup = new Startup();

            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            startup.Configure(serviceProvider.GetService<ILoggerFactory>(), serviceProvider);

            WaitUntilExit(shutdown: () => (serviceProvider as IDisposable)?.Dispose());
        }

        /// <summary>
        /// Blocks until SIGTERM or SIGINT and then performs graceful shutdown.
        /// </summary>
        private static void WaitUntilExit(Action shutdown)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            AssemblyLoadContext.GetLoadContext(typeof(Program).GetTypeInfo().Assembly).Unloading += context =>
            {
                shutdown();
                cancellationTokenSource.Cancel();
            };
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                shutdown();
                cancellationTokenSource.Cancel();
                eventArgs.Cancel = true;
            };

            Console.WriteLine("Application started. Press Ctrl+C to shut down.");
            cancellationTokenSource.Token.WaitHandle.WaitOne();
        }
    }
}
