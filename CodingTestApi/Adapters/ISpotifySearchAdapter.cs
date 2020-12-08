using System.Threading.Tasks;
using CodingTestApi.Models.Spotify;

namespace CodingTestApi.Adapters
{
    public interface ISpotifySearchAdapter
    {
        /// <summary>
        /// Gets artists from the spotify API using <see paramref="query"/>.
        /// </summary>
        /// <param name="query">The query used for looking up artists.</param>
        /// <returns>The artists response.</returns>
        Task<ArtistsSearchResponse> GetArtistsAsync(string query);
    }
}