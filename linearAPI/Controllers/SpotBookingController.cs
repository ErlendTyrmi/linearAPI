
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
        private readonly ILinearAccess<LinearSpotBooking> spotBookingRepo;
        private readonly ILinearAccess<LinearOrder> orderRepo;
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
           var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);

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
           var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = spotBookingRepo.ReadAll();
            if (data == null) return StatusCode(404);

            if (data.Count < 1) return StatusCode(204); // Not found

            if (user.IsAdmin) return Ok(data);

            var filteredData = data.Where((it) => it.AgencyId == user.AgencyId);

            return Ok(filteredData);
        }

        //[HttpGet]
        //[Route("all")]
        //[Produces("application/json")]
        //public IActionResult Get()
        //{
        //   var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
        //    if (user == null) return StatusCode(401);

        //    if (!user.IsAdmin) return StatusCode(403);

        //    var data = spotBookingRepo.ReadAll();
        //    if (data == null) return StatusCode(404); 
        //     if (data.Count < 1) return StatusCode(204); // Not found

        //    return Ok(data);
        //}
    }
}