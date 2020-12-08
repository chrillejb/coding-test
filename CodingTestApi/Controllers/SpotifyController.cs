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
        private readonly ArtistMatchingService _artistMatchingService;

        public SpotifyController(ArtistMatchingService artistMatchingService)
        {
            _artistMatchingService = artistMatchingService;
        }

        [HttpGet("artist")]
        public async Task<ActionResult<Artist>> GetArtistAsync(string artistName)
        {
            var artist = await _artistMatchingService.GetSingleMatchingArtistAsync(artistName);

            return Ok(artist);
        }
    }
}