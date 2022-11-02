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
        private readonly LinearAccess<LinearAgency> agencyRepo;

        public AgencyController(ILogger<AgencyController> logger, ISessionService sessionService, ILinearRepo repo)
        {
            this.logger = logger;
            this.agencyRepo = repo.Agency;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            var data = agencyRepo.Read(id);
            if (data == null) return StatusCode(404);

            return Ok(data);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var data = agencyRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}