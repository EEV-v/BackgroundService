using System;
using Newtonsoft.Json;

namespace BackgroundService.BusinessLogic.Contract.Models
{
    public class Token
    {
        public Token(string access_token, string token_type)
        {
            AccessToken = access_token ?? throw new ArgumentNullException(nameof(access_token));
            TokenType = token_type ?? throw new ArgumentNullException(nameof(token_type));
        }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; }
    }
}
