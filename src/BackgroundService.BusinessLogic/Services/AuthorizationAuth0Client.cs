using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BackgroundService.BusinessLogic.Constants;
using BackgroundService.BusinessLogic.Contract.Exceptions;
using BackgroundService.BusinessLogic.Contract.Models;
using BackgroundService.BusinessLogic.Contract.Services;
using BackgroundService.BusinessLogic.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BackgroundService.BusinessLogic.Services
{
    public class AuthorizationAuth0Client : IAuthorizationClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthSettings _authSettings;
        private readonly ILogger<AuthorizationAuth0Client> _logger;

        public AuthorizationAuth0Client(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<AuthSettings> authSettings,
            ILogger<AuthorizationAuth0Client> logger)
        {
            _httpClient = httpClientFactory?.CreateClient(HttpClientsNames.TOKEN)
                ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _authSettings = authSettings?.CurrentValue
                ?? throw new ArgumentNullException(nameof(authSettings));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Token> GetToken()
        {
            var content = JsonConvert.SerializeObject(
                new
                {
                    client_id = _authSettings.ClientId,
                    client_secret = _authSettings.ClientSecret,
                    audience = _authSettings.BaseUrl + "api/v2/",
                    grant_type = "client_credentials"
                }, Formatting.None);

            var response = await _httpClient.PostAsync(
                new Uri("oauth/token", UriKind.Relative),
                new StringContent(
                    content,
                    Encoding.UTF8,
                    "application/json")
                );

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationGetTokenException(
                    response.RequestMessage.ToString());
            }

            var token = JsonConvert.DeserializeObject<Token>(
                await response.Content.ReadAsStringAsync());

            return token;
        }
    }
}
