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
    public class AdvertiserController : ControllerBase
    {
        private readonly ILogger<AdvertiserController> _logger;
        private readonly LinearRepo<LinearAdvertiser> advertiserRepo = new LinearRepo<LinearAdvertiser>("Generated/");
        private readonly SessionService sessionService = SessionService.GetRepo();

        public AdvertiserController(ILogger<AdvertiserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            LinearUser? user = sessionService.getUser(userName);
            if (user == null)
            {
                _logger.LogError($"Expected valid user with username, but {userName} not found by {nameof(SessionService)}.");
                return StatusCode(500);
            }

            var advertiser = advertiserRepo.Read(id);
            if (advertiser == null) return StatusCode(404);

            if (user.IsAdmin) return Ok(advertiser);

            if (advertiser.AgencyId != user.AgencyId) return StatusCode(403);

            return Ok(advertiser);
        }

        [HttpGet]
        [Route("mine")]
        [Produces("application/json")]
        public IActionResult GetByUser(string userId)
        {
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            LinearUser? user = sessionService.getUser(userName);
            if (user == null)
            {
                _logger.LogError($"Expected valid user with username, but {userName} not found by {nameof(SessionService)}.");
                return StatusCode(500);
            }

            var allAdvertisers = advertiserRepo.ReadAll();
            if (allAdvertisers == null) return StatusCode(404);

            if (user.IsAdmin) return Ok(allAdvertisers);

            var advertisers = allAdvertisers.Where((it) =>  it.AgencyId == user.AgencyId);

            return Ok(advertisers);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            LinearUser? user = sessionService.getUser(userName);
            if (user == null)
            {
                _logger.LogError($"Expected valid user with username, but {userName} not found by {nameof(SessionService)}.");
                return StatusCode(500);
            }

            if (!user.IsAdmin) return StatusCode(403);

            var data = advertiserRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}