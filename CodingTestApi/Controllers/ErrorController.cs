using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodingTestApi.Controllers
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        [Route("")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var exceptionThatOccurred = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred. Message: '{exceptionThatOccurred?.Message}'");
        }
    }
}