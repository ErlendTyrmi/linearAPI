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
    public class AdvertiserController : ControllerBase
    {
        private readonly ILogger<AdvertiserController> logger;
        private readonly LinearAccess<LinearAdvertiser> advertiserRepo;
        private readonly ISessionService sessionService;

        public AdvertiserController(ILogger<AdvertiserController> logger, ISessionService sessionService, ILinearRepo repo)
        {
            this.logger = logger;
            this.sessionService = sessionService;
            this.advertiserRepo = repo.Advertiser;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var advertiser = advertiserRepo.Read(id);
            if (advertiser == null) return StatusCode(404);

            if (user.IsAdmin) return Ok(advertiser);

            if (advertiser.AgencyId != user.AgencyId) return StatusCode(403);

            return Ok(advertiser);
        }

        [HttpGet]
        [Route("own")]
        [Produces("application/json")]
        public IActionResult GetByAgency()
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var allAdvertisers = advertiserRepo.ReadAll();
            if (allAdvertisers == null) return StatusCode(404);

            var advertisers = allAdvertisers.Where((it) =>  it.AgencyId == user.AgencyId);

            return Ok(advertisers);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            if (!user.IsAdmin) return StatusCode(403);

            var data = advertiserRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}