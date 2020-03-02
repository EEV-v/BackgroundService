using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundService.BusinessLogic.Jobs.Base
{
    public abstract class ProcessingJob<TService>
        : IHostedService
    {
        protected IServiceProvider Services { get; }
        protected readonly ILogger Logger;
        private readonly int _intervalInSeconds;
        public ProcessingJob(
            IServiceProvider services,
            ILogger logger,
            int intervalInSeconds)
        {
            Services = services;
            Logger = logger;
            _intervalInSeconds = intervalInSeconds;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Logger.LogInformation(
                  $"Job started: {DateTime.UtcNow}");

                using (var scope = Services.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<TService>();
                    try
                    {
                        await ExecuteAsync(service, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex.ToString());
                    }

                }
                Logger.LogInformation(
                    $"Job finished: {DateTime.UtcNow}");

                await Task.Delay(
                    TimeSpan.FromSeconds(
                        _intervalInSeconds),
                    stoppingToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected abstract Task ExecuteAsync(TService service, CancellationToken stoppingToken);
    }
}
