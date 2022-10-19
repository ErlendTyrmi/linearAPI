using linearAPI.Authorization;
using linearAPI.Entities;
using linearAPI.Repo;
using linearAPI.Services;
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
        private SessionService sessionRepo = SessionService.GetRepo();

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult getSessionUser()
        {
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            var user = SessionService.GetRepo().getUser(userName);
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
                var user = sessionRepo.getUser(data.username);
                if (user == null)
                {
                    _logger.LogError("Failed login: No such user");
                    return StatusCode(401);
                }

                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String)};
                var userIdentity = new ClaimsIdentity(claims, "sessionIdentity");

                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(userIdentity)))
                    .Wait();

                return Ok(user);
            }

            _logger.LogError("Failed login");
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
                _logger.LogError(ex.StackTrace);
                return StatusCode(500);
            }
        }
    }
}