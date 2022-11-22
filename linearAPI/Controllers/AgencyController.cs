using Common.Interfaces;
using LinearAPI.Services;
using LinearEntities.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;


namespace Entities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AgencyController : ControllerBase
    {
        private readonly ILogger<AgencyController> logger;
        private readonly ILinearAccess<Agency> agencyRepo;
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
        public IActionResult Get()
        {
           var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = agencyRepo.Read(user.AgencyId);
            if (data == null) return StatusCode(404);

            return Ok(data);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
           var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = agencyRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}