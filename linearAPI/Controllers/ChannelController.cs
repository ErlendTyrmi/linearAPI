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
    public class ChannelController : ControllerBase
    {
        private readonly ILogger<ChannelController> logger;
        private readonly LinearAccess<LinearChannel> ChannelRepo;

        public ChannelController(ILogger<ChannelController> logger, ILinearRepo repo)
        {
            this.logger = logger;
            this.ChannelRepo = repo.Channel;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var data = ChannelRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}