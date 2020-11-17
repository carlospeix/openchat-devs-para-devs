using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenChat.Model;

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
            var system = new OpenChatSystem();

            var user = system.RegisterUser(request.username, request.password, request.about);

            return new CreatedResult("", new UserResult(user));
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
    public class UserResult
    {
        public UserResult(User user)
        {
            userId = user.Id;
            username = user.Name;
            about = user.About;
        }

        public Guid userId;
        public string username;
        public string about;
    }
}
