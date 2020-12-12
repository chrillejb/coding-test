using System.Linq;
using System.Threading.Tasks;
using CodingTestApi.Adapters;
using CodingTestApi.Exceptions;
using CodingTestApi.Extensions;
using CodingTestApi.Models;

namespace CodingTestApi.Services
{
    public class ArtistMatchingService
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;

        public ArtistMatchingService(ISpotifySearchAdapter spotifySearchAdapter)
        {
            _spotifySearchAdapter = spotifySearchAdapter;
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
            // TODO add case for empty "items" from API response => 404
            var artistString = artistNameQuery.ToArtistString();

            var artistsResponse = await _spotifySearchAdapter.GetArtistsAsync(artistString);

            var matchingArtist = artistsResponse.Artists.Items.FirstOrDefault(
                artistItem => artistItem.Name.ToArtistString().Equals(artistString)) ?? 
                throw new NoMatchingArtistException("Could not find matching artist.");

            return new Artist
            {
                Id = matchingArtist.Id,
                Name = matchingArtist.Name
            };
        }
    }
}