using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using linearAPI.Entities;

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

        [HttpGet]
        [Route("login")]
        [Produces("application/json")]
        public IActionResult Login()
        {

            HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity("12345"))).Wait();

            return Ok(new LinearUser("Jens Testa"));
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