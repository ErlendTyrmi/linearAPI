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
    public class SpotController : ControllerBase
    {
        private readonly ILogger<SpotController> _logger;
        private LinearRepo<LinearSpot> dataRepo = new LinearRepo<LinearSpot>("Generated/");

        public SpotController(ILogger<SpotController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var data = dataRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}