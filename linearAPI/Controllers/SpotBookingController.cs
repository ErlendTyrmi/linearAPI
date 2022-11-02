using linearAPI.Entities;
using linearAPI.Entities.BaseEntity;
using linearAPI.Repo;
using linearAPI.Repo.Database;
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
        private readonly ILogger<SpotBookingController> logger;
        private readonly LinearAccess<LinearSpotBooking> spotBookingRepo;
        private readonly ISessionService sessionService;

        public SpotBookingController(ILogger<SpotBookingController> logger, ISessionService sessionService, ILinearRepo repo)
        {
            this.logger = logger;
            this.sessionService = sessionService;
            this.spotBookingRepo = repo.SpotBooking;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            // TODO: Check that user is handler for order
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            var data = spotBookingRepo.Read(id);
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

            var data = spotBookingRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}