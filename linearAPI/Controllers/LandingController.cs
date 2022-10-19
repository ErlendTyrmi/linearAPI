
using linearAPI.Services.CookieAuthorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace linearAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class LandingController : ControllerBase
    {
        private readonly ILogger<LandingController> _logger;

        public LandingController(ILogger<LandingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetLanding")]
        public IActionResult Get()
        {
            var cookie = HttpContext.Request.Cookies["session_cookie"];

            if (cookie == null) return StatusCode(401);

            new CookieDatabaseImpl_mock().GetUser(cookie);

            return Ok(DateTime.Now);
        }
    }
}