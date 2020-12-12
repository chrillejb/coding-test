using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodingTestApi.Models.Spotify;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CodingTestApi.Auth
{
    ///<summary>
    /// Used to fetch bearer tokens for the Spotify API.
    ///</summary>
    public class SpotifyTokenFetcher
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly HttpClient _httpClient;

        public SpotifyTokenFetcher(HttpClient httpClient, IConfiguration configuration)
        {
            _clientId = configuration["Spotify:ClientId"];
            _clientSecret = configuration["Spotify:ClientSecret"];

            if (string.IsNullOrEmpty(_clientId) ||
                string.IsNullOrEmpty(_clientSecret))
            {
                throw new Exception($"({nameof(SpotifyTokenFetcher)}) Client ID and Client Secret may not be unset or empty. Please make sure the credentials have been configured correctly.");
            }

            _httpClient = httpClient;
        }

        ///<summary>
        /// Fetches a token from the Spotify API token endpoint.
        ///</summary>
        ///<returns>A bearer token that can be used for accessing the Spotify API.</returns>
        public async Task<string> FetchTokenAsync()
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = _httpClient.BaseAddress,
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"}
                })
            };
            string basicAuthCreds = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
            httpRequest.Headers.Add("Authorization", $"Basic {basicAuthCreds}");

            var httpResponseMessage = await _httpClient.SendAsync(httpRequest);
            httpResponseMessage.EnsureSuccessStatusCode();
            var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(stringResponse);
            return tokenResponse.AccessToken;
        }
    }
}