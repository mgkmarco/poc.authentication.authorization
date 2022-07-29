using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace poc_auth_service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("bam")]
        public Task Login()
        {
            var cool = "sdasd";
            return Task.CompletedTask;
        }

        [Authorize]
        [HttpGet]
        public Task Hello()
        {
            var h = HttpContext.User.Claims;

            return Task.CompletedTask;
        }

        [Authorize(Roles = "superuser")]
        [HttpGet("hello")]
        public Task Helo()
        {
            var h = HttpContext.User.Claims;

            return Task.CompletedTask;
        }

        [Authorize(Roles = "superuser2")]
        [HttpGet("hello2")]
        public Task Helo2()
        {
            var h = HttpContext.User.Claims;

            return Task.CompletedTask;
        }
    }
}
