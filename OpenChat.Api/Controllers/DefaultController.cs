using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

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

        [HttpPost("/openchat/registration")]
        public ObjectResult RegisterUser([FromBody] RegistrationRequest request)
        {
            var user = new { userId = Guid.Empty, username = request.username, about = request.about };

            return new CreatedResult("", user);
        }
    }
    public class RegistrationRequest
    {
        public RegistrationRequest(string userName, string password, string about)
        {
            this.username = userName;
            this.password = password;
            this.about = about;
        }

        public string username;
        public string password;
        public string about;
    }
}
