using linearAPI.Entities;
using linearAPI.Entities.BaseEntity;
using linearAPI.Repo;
using linearAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;


namespace linearAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SpotBookingController : ControllerBase
    {
        private readonly ILogger<LandingController> _logger;
        private LinearRepo<LinearSpotBooking> dataRepo = new LinearRepo<LinearSpotBooking>("Generated/");
        private SessionService sessionRepo = SessionService.GetRepo();

        public SpotBookingController(ILogger<LandingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            // TODO: Check that user is handler for order
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            var data = dataRepo.Read(id);
            if (data == null) return StatusCode(404);

            return Ok(data);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            // TODO: Check that user is handler for order
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            var data = dataRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}