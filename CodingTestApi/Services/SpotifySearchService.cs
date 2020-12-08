using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodingTestApi.Auth;
using CodingTestApi.Models;
using CodingTestApi.Models.Spotify;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace CodingTestApi.Services
{
    public class SpotifySearchService
    {
        private readonly HttpClient _httpClient;
        private readonly SpotifyTokenFetcher _spotifyTokenFetcher;

        public SpotifySearchService(HttpClient httpClient, SpotifyTokenFetcher spotifyTokenFetcher)
        {
            _httpClient = httpClient;
            _spotifyTokenFetcher = spotifyTokenFetcher;
        }

        /// <summary>
        /// Performs a search using the Spotify API for a maching artist using <see paramref="artistNameQuery"/>.
        /// The method will ignore white spaces and punctionation in the query.
        /// The query is also insensitive.
        /// </summary>
        /// <param name="artistNameQuery">The string used to query the Spotify API.</param>
        /// <returns>The single matching artist.</returns>
        public async Task<Artist> GetSingleMatchingArtistAsync(string artistNameQuery)
        {
            var token = await _spotifyTokenFetcher.FetchTokenAsync();

            var queryString = new Dictionary<string, string>()
            {   
                // TODO extenstion methods! (stripWhiteSpaces().stripPunctuationChars())
                {"q", artistNameQuery},
                {"type", "artist"}
            };

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(QueryHelpers.AddQueryString(_httpClient.BaseAddress.ToString(), queryString)),
                Method = HttpMethod.Get
            };
            httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");

            // TODO add case for empty "items" from API response => 404
            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            var artistsSearchResponse = JsonConvert.DeserializeObject<ArtistsSearchResponse>(
                await httpResponseMessage.Content.ReadAsStringAsync());
            // TODO match with search argument
            // TODO return artist name (and id) as fetched from Spotify search

            return new Artist
            {
                ArtistId = "123",
                ArtistName = "hello artist"
            };
        }
    }
}