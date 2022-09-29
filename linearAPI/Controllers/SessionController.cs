using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using linearAPI.Entities;

using linearAPI.Services.CookieAuthorization;
using Microsoft.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Database.CookieAuthorization;
using Database;
using Database.LinearDatabase;

namespace linearAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("user")]
        [Produces("application/json")]

      
        public IActionResult User()
        {
            CookieDatabase cookieDb = new CookieDatabase();

            // Get user form cookiedatabase
            var cookie = HttpContext.Request.Cookies["session_cookie"];

            if (cookie == null) return StatusCode(401);

            //var user = new CookieDatabase().GetUser(cookie);

            //if (user == null) return StatusCode(401);

            return Ok(new LinearUser("User", "method", "implemented?", false));
        }

        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LinearCredentials data)
        {
            if (LinearAuthorization.Authorize(data))
            {

                // Find user
                var user = new LinearUser("NoId", "NoName", "NoEmail", false);

                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(data.username)))
                    .Wait();

                // Add user to sessions
                new CookieDatabase().SetSession(data.username);

                // Cleanup sessions


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