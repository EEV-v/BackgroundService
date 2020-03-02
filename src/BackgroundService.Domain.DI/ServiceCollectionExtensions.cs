using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BackgroundService.Domain.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContextPool<BackgroundServiceDbContext>((provider, options) =>
                {
                    var connectionString = configuration
                        .GetConnectionString("DefaultConnection");

                    options
                        .UseSqlServer(connectionString);
                });
            return services;
        }
    }
}
