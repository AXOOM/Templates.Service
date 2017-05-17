using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nito.AsyncEx;
using Polly;

namespace MySolutionName
{
    public class Program
    {
        public static void Main() => AsyncContext.Run(MainAsync);

        private static async Task MainAsync()
        {
            var provider = BuildServiceProvider();

            var startupLogger = provider.GetService<ILogger<Program>>();
            var policy = Policy
                .Handle<SocketException>()
                .WaitAndRetryAsync(
                    sleepDurations: new[] {TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10)},
                    onRetry: (ex, timeSpan) => startupLogger.LogWarning(0, ex, "Problem connecting to external service. Retrying in {0}.", timeSpan));

            await policy.ExecuteAsync(async () =>
            {
                // TODO: Implement service functionality
                await WaitUntilCancelAsync();
            });
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var provider = new ServiceCollection()
                .AddLogging()
                .AddOptions()
                //.Configure<MyOptions>(config.GetSection("MyOptions"))
                .BuildServiceProvider();

            provider.GetService<ILoggerFactory>()
                .AddConsole(config.GetSection("Logging"));

            return provider;
        }

        private static async Task WaitUntilCancelAsync()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, e) =>
            {
                cancellationTokenSource.Cancel();
                e.Cancel = true;
            };

            while (true)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(10), cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}