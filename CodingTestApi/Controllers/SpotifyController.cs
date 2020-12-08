using System;
using System.Threading.Tasks;
using CodingTestApi.Exceptions;
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

        /// <summary>
        /// Tries to get an artist based on the provided artist name.
        /// </summary>
        /// <param name="artistName"></param>
        /// <response code="200">Successful operation.</response>
        /// <response code="204">The request was received, but no match was found.</response>
        [HttpGet("artist")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(204)]
        public async Task<ActionResult<Artist>> GetArtistAsync(string artistName)
        {
            // TODO null check => 400

            try
            {
                var artist = await _artistMatchingService.GetSingleMatchingArtistAsync(artistName);
                return Ok(artist);
            }
            catch(NoMatchingArtistException noMatchingArtistException)
            {
                Console.WriteLine(noMatchingArtistException.Message);
                return NoContent();
            }
        }
    }
}