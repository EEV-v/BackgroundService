using System;
using BackgroundService.BusinessLogic.Constants;
using BackgroundService.BusinessLogic.Contract.Services;
using BackgroundService.BusinessLogic.Services;
using BackgroundService.BusinessLogic.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackgroundService.BusinessLogic.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogicServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AuthSettings>(
                configuration.GetSection(nameof(AuthSettings)));
            services.Configure<HealthCheckJobSettings>(
                configuration.GetSection(nameof(HealthCheckJobSettings)));
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IAuthorizationClient, AuthorizationAuth0Client>();

            AddJobs(services);

            AddHttpClients(services, configuration);

            return services;
        }

        private static void AddJobs(IServiceCollection services)
        {
            services.AddHostedService<Jobs.HealthCheckJob>();
        }

        private static void AddHttpClients(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddHttpClient(HttpClientsNames.TOKEN, (serviceProvider, client) =>
            {
                var settings = serviceProvider
                    .GetService<IOptionsMonitor<AuthSettings>>()
                    .CurrentValue;
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient(HttpClientsNames.API, (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(configuration["api_1:url"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
