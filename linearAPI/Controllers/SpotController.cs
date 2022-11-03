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
    public class SpotController : ControllerBase
    {
        private readonly ILogger<SpotController> _logger;
        private readonly LinearAccess<LinearSpot> SpotRepo;
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
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = SpotRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}