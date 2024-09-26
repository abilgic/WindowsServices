using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsServices
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register your services here
                    services.AddHostedService<Worker>(); // Example background worker
                })
                .UseWindowsService()
                .Build()
                .RunAsync();
        }
    }

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                // Log a message to indicate the service is running
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Simulate some work
                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation("Worker stopping.");
        }
    }
}
