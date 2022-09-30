using Database;
using Database.CookieAuthorization;
using Database.LinearDatabase;
using linearAPI.Entities;
using linearAPI.Services.CookieAuthorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace linearAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private LinearRepo<LinearUser> userRepo = new LinearRepo<LinearUser>();
        private SessionDatabase sessionRepo = SessionDatabase.GetRepo();

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        [Produces("application/json")]

      
        public new IActionResult getSession()
        {
            // Get user form cookiedatabase
            var claims = HttpContext.User.Identity?.AuthenticationType;
            if (claims == null) return StatusCode(401);

            // Check session
            var username = sessionRepo.GetSession(claims);
            if (username == null) return StatusCode(401);

            // Find user
            var user = userRepo.Read(username);
            if (user == null) return StatusCode(401);

            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LinearCredentials data)
        {
            if (LinearAuthentication.AuthenticateCredentials(data))
            {
                // Find user
                var user = userRepo.Read(data.username);
                if (user == null) return StatusCode(401);

                // Add user to sessions
                sessionRepo.SetSession(data.username);

                // Cleanup sessions
                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(data.username)))
                    .Wait();

                return Ok(user);
            }

            return StatusCode(401);
        }

        [HttpGet]
        [Route("logout")]
        [Produces("application/json")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}