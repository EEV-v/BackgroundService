using System;
using System.Threading;
using System.Threading.Tasks;
using BackgroundService.BusinessLogic.Contract.Services;
using BackgroundService.BusinessLogic.Jobs.Base;
using BackgroundService.BusinessLogic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BackgroundService.BusinessLogic.Jobs
{
    public class HealthCheckJob : ProcessingJob<IApiService>
    {
        public HealthCheckJob(
            IServiceProvider services,
            ILogger<HealthCheckJob> logger,
            IOptionsMonitor<HealthCheckJobSettings> healthCheckJobSettings)
            : base(services, logger, healthCheckJobSettings.CurrentValue.Interval)
        {
        }

        protected override async Task ExecuteAsync(
            IApiService apiService,
            CancellationToken stoppingToken)
        {
            await apiService.CheckHealth();
        }
    }
}
