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
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult User()
        {
            CookieDatabase cookieDb = new CookieDatabaseImpl_mock();

            //// Get credentials
            //string encodedEmailPassword = authHeader.Substring("Basic ".Length).Trim();
            //string emailPassword = Encoding
            //.GetEncoding("iso-8859-1")
            //.GetString(Convert.FromBase64String(encodedEmailPassword));

            //// Get email and password
            //int seperatorIndex = emailPassword.IndexOf(':');
            //string email = emailPassword.Substring(0, seperatorIndex);
            //string password = emailPassword.Substring(seperatorIndex + 1);

            return Ok(new LinearUser("Jens Testa"));
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

                return Ok(new LinearUser("Jens Testa"));
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