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
            CookieDatabase cookieDb = new CookieDatabaseImpl_mock();

            // Get user form cookiedatabase
            var cookie = HttpContext.Request.Cookies.SingleOrDefault();

            return Ok(new LinearUser("22222", "Jens Testa", "a@b.com", false));
        }

        [HttpPost]
        [Route("login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LinearCredentials data)
        {
            if (LinearAuthorization.Authorize(data))
            {
                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity("12345")))
                    .Wait();

                return Ok(new LinearUser("33333", "Jens Amazonas", "att@booking.mix", false));
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