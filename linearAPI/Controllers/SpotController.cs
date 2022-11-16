
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
    public class SpotController : ControllerBase
    {
        private readonly ILogger<SpotController> _logger;
        private readonly ILinearAccess<LinearSpot> SpotRepo;
        private readonly ISessionService sessionService;

        public SpotController(ILogger<SpotController> logger, ILinearRepo repo, ISessionService sessionService)
        {
            _logger = logger;
            this.SpotRepo = repo.Spot;
            this.sessionService = sessionService;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get()
        {
           var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = SpotRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}