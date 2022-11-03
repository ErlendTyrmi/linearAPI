using linearAPI.Entities;
using linearAPI.Entities.BaseEntity;
using linearAPI.Repo;
using linearAPI.Repo.Database;
using linearAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace linearAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SpotBookingController : ControllerBase
    {
        private readonly ILogger<SpotBookingController> logger;
        private readonly LinearAccess<LinearSpotBooking> spotBookingRepo;
        private readonly LinearAccess<LinearOrder> orderRepo;
        private readonly ISessionService sessionService;

        public SpotBookingController(ILogger<SpotBookingController> logger, ISessionService sessionService, ILinearRepo repo)
        {
            this.logger = logger;
            this.sessionService = sessionService;
            this.spotBookingRepo = repo.SpotBooking;
            this.orderRepo = repo.Order;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            // TODO: Check that user is handler for order
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = spotBookingRepo.Read(id);
            if (data == null) return StatusCode(404);

            return Ok(data);
        }

        [HttpGet]
        [Route("own")]
        [Produces("application/json")]
        public IActionResult GetByUSerId()
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = spotBookingRepo.ReadAll();
            if (data == null) return StatusCode(404);

            if (user.IsAdmin) return Ok(data);

            var filteredData = data.Where((it) => it.AgencyId == user.AgencyId);

            return Ok(filteredData);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            if (!user.IsAdmin) return StatusCode(403);

            var data = spotBookingRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}