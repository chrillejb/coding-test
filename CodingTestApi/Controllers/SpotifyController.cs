using System.Threading.Tasks;
using CodingTestApi.Models;
using CodingTestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly SpotifySearchService _spotifySearchService;

        public SpotifyController(SpotifySearchService spotifySearchService)
        {
            _spotifySearchService = spotifySearchService;
        }

        [HttpGet("artist")]
        public async Task<ActionResult<Artist>> GetArtistAsync(string artistName)
        {
            var artist = await _spotifySearchService.GetSingleMatchingArtistAsync(artistName);

            return Ok(artist);
        }
    }
}