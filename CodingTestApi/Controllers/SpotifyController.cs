using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly SpotifySearch _spotifySearch;

        public SpotifyController(SpotifySearch spotifySearch)
        {
            _spotifySearch = spotifySearch;
        }

        [HttpGet("artist")]
        public async Task<ActionResult<Artist>> GetArtistAsync(string artistName)
        {
            var artist = await _spotifySearch.GetSingleMatchingArtistAsync(artistName);

            return Ok(artist);
        }
    }
}