using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        [HttpGet("artist")]
        public async Task<ActionResult<Artist>> GetArtist(string artistName)
        {
            return Ok(new Artist
            {
                ArtistId = "123",
                ArtistName = "hello artist"
            });
        }
    }
}