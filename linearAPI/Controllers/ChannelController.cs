
using Common.Interfaces;
using LinearAPI.Services;
using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
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
    public class ChannelController : ControllerBase
    {
        private readonly ILogger<ChannelController> logger;
        private readonly ILinearAccess<LinearChannel> ChannelRepo;
        private readonly ISessionService sessionService;

        public ChannelController(ILogger<ChannelController> logger, ILinearRepo repo, ISessionService sessionService)
        {
            this.logger = logger;
            this.ChannelRepo = repo.Channel;
            this.sessionService = sessionService;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get()
        {
           var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = ChannelRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}