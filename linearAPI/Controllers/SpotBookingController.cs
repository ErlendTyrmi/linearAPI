
using Common.Interfaces;
using LinearAPI.Services;
using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Entities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SpotBookingController : ControllerBase
    {
        private readonly ILogger<SpotBookingController> logger;
       
        private readonly ISessionService sessionService;
        private readonly ISpotBookingService spotBookingService;

        public SpotBookingController(ILogger<SpotBookingController> logger, ISessionService sessionService, ISpotBookingService spotBookingService)
        {
            this.logger = logger;
            this.sessionService = sessionService;
            this.spotBookingService = spotBookingService;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = spotBookingService.Get(id);
            if (data == null) return StatusCode(404);

            return Ok(data);
        }

        [HttpGet]
        [Route("own")]
        [Produces("application/json")]
        public IActionResult GetAllByUser()
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var bookings = spotBookingService.GetByUser(user);
            if (bookings == null) return StatusCode(404);

            return Ok(bookings);
        }

        [HttpDelete]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Delete(SpotBooking booking)
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            int status = spotBookingService.deleteBooking(booking, user);

            if (status != 200) return StatusCode(status);

            return Ok(booking);
        }
    }
}