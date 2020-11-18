using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenChat.Model;

namespace OpenChat.Api.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly OpenChatSystem system;
        private readonly ILogger<DefaultController> logger;

        public DefaultController(OpenChatSystem system, ILogger<DefaultController> logger)
        {
            this.system = system;
            this.logger = logger;
        }

        [HttpGet("/openchat/")]
        public ObjectResult GetStatus()
        {
            return new ObjectResult(new { status = "Up" });
        }

        [HttpPost("/openchat/registration")]
        public ObjectResult RegisterUser([FromBody] RegistrationRequest request)
        {
            return DispatchRequest(
                () => system.RegisterUser(request.username, request.password, request.about),
                (user) => new CreatedResult($"/openchat/users/{user.Id}", new UserResult(user)));
        }

        [HttpPost("/openchat/login")]
        public ObjectResult LoginUser([FromBody] LoginRequest request)
        {
            try
            {
                var user = system.LoginUser(request.username, request.password);
                return new OkObjectResult(new UserResult(user));
            }
            catch (InvalidOperationException ioe)
            {
                return new BadRequestObjectResult(ioe.Message);
            }
        }

        public ObjectResult DispatchRequest<T>(Func<T> action, Func<T, ObjectResult> success)
        {
            try
            {
                return success(action.Invoke());
            }
            catch (InvalidOperationException ioe)
            {
                return new BadRequestObjectResult(ioe.Message);
            }
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
    public class LoginRequest
    {
        public LoginRequest(string userName, string password)
        {
            this.username = userName;
            this.password = password;
        }

        public string username;
        public string password;
    }
}