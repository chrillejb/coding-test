using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodingTestApi.Auth;
using CodingTestApi.Models.Spotify;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace CodingTestApi.Adapters
{
    public class SpotifySearchAdapter : ISpotifySearchAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly SpotifyTokenFetcher _spotifyTokenFetcher;

        public SpotifySearchAdapter(HttpClient httpClient, SpotifyTokenFetcher spotifyTokenFetcher)
        {
            _httpClient = httpClient;
            _spotifyTokenFetcher = spotifyTokenFetcher;
        }

        public async Task<ArtistsSearchResponse> GetArtistsAsync(string query)
        {
            var token = await _spotifyTokenFetcher.FetchTokenAsync();

            var queryString = new Dictionary<string, string>()
            {
                {"q", query},
                {"type", "artist"}
            };

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(QueryHelpers.AddQueryString(_httpClient.BaseAddress.ToString(), queryString)),
                Method = HttpMethod.Get
            };
            httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");
            
            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            return JsonConvert.DeserializeObject<ArtistsSearchResponse>(
                await httpResponseMessage.Content.ReadAsStringAsync());
        }
    }
}