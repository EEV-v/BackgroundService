using System.Threading;
using System.Threading.Tasks;
using BackgroundService.BusinessLogic.Contract.Services;
using Microsoft.Extensions.Logging;

namespace BackgroundService.BusinessLogic.Services
{
    public class ApiService : IApiService
    {
        private readonly ILogger _logger;
        private readonly IAuthorizationClient _authorizationClient;

        public ApiService(
            ILogger<ApiService> logger,
            IAuthorizationClient authorizationClient)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _authorizationClient = authorizationClient ?? throw new System.ArgumentNullException(nameof(authorizationClient));
        }

        public async Task CheckHealth()
        {
            var token = await _authorizationClient.GetToken();
        }
    }
}
