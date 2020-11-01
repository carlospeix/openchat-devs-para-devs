using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OpenChat.Api.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public DefaultController(ILogger<DefaultController> logger)
        {
        }

        [HttpGet("/openchat/")]
        public ObjectResult GetStatus()
        {
            return new ObjectResult(new { status = "Up" });
        }
    }
}
