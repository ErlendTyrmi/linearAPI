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
    public class AgencyController : ControllerBase
    {
        private readonly ILogger<AgencyController> logger;
        private readonly ILinearAccess<LinearAgency> agencyRepo;
        private readonly ISessionService sessionService;

        public AgencyController(ILogger<AgencyController> logger, ISessionService sessionService, ILinearRepo repo)
        {
            this.logger = logger;
            this.sessionService = sessionService;
            this.agencyRepo = repo.Agency;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = agencyRepo.Read(id);
            if (data == null) return StatusCode(404);

            return Ok(data);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = agencyRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}