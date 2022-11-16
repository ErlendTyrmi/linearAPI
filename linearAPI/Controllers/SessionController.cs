using Entities.Authorization;
using LinearAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Entities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> logger;
        private readonly ISessionService sessionService;

        public SessionController(ILogger<SessionController> logger, ISessionService sessionService)
        {
            this.logger = logger;
            this.sessionService = sessionService;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult getSessionUser()
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);

            if (user == null) return StatusCode(500);
           
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
                var user = sessionService.getUserFromUserName(data.Username);
                if (user == null)
                {
                    logger.LogError("Failed login: No such user: " + data.Username);
                    return StatusCode(401);
                }

                var sessionId = sessionService.SignIn(user);

                var claims = new List<Claim> {
                new Claim(ClaimTypes.Sid, sessionId, ClaimValueTypes.String)};
                var userIdentity = new ClaimsIdentity(claims, "sessionIdentity");

                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(userIdentity)))
                    .Wait();

               return Ok(user);
            }

            logger.LogError("Failed login: Credentials rejected.");
            return StatusCode(401);
        }

        [HttpGet]
        [Route("logout")]
        [Produces("application/json")]
        public IActionResult Logout()
        {
            string? id = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (id != null) sessionService.SignOut(id);

            try
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.StackTrace);
                return StatusCode(500);
            }
        }
    }
}