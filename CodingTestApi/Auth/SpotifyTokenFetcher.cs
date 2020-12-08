using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

///<summary>
/// Used to fetch bearer tokens for the Spotify API.
///</summary>
public class SpotifyTokenFetcher
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _spotifyTokenUrl;
    private readonly HttpClient _httpClient;

    public SpotifyTokenFetcher(IConfiguration configuration)
    {
        _clientId = configuration["Spotify:ClientId"];
        _clientSecret = configuration["Spotify:ClientSecret"];
        _spotifyTokenUrl = configuration["Urls:SpotifyTokenUrl"];

        if (string.IsNullOrEmpty(_clientId) ||
            string.IsNullOrEmpty(_clientSecret))
        {
            throw new Exception($"ClientId and ClientSecret may not be unset or empty. Please make sure the credentials have been configured correctly.");
        }

        _httpClient = new HttpClient();
    }

    ///<summary>
    /// Fetches a token from the Spotify API token endpoint.
    ///</summary>
    ///<returns>A bearer token that can be used for accessing the Spotify API.</returns>
    public async Task<string> FetchTokenAsync()
    {
        var httpRequest = new HttpRequestMessage
        {
            RequestUri = new Uri(_spotifyTokenUrl),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            })
        };
        string basicAuthCreds = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes($"{_clientId}:{_clientSecret}"));
        httpRequest.Headers.Add("Authorization", $"Basic {basicAuthCreds}");

        var httpResponse = await _httpClient.SendAsync(httpRequest);
        var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(stringResponse);
        return tokenResponse.AccessToken;
    }
}