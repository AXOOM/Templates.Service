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
        /// Blocks until SIGTERM or SIGINT is raised and then calls the <paramref name="shutdown"/> callback.
        /// </summary>
        private static void WaitUntilExit(Action shutdown)
        {
            var wait = new ManualResetEventSlim(initialState: false);

            AssemblyLoadContext.GetLoadContext(typeof(Program).GetTypeInfo().Assembly).Unloading += context =>
            {
                shutdown();
                wait.Set();
            };
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                shutdown();
                wait.Set();
                eventArgs.Cancel = true;
            };

            Console.WriteLine("Application started. Press Ctrl+C to shut down.");
            wait.Wait();
        }
    }
}
