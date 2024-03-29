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
    public class AdvertiserController : ControllerBase
    {
        private readonly ILogger<AdvertiserController> logger;
        private readonly ILinearAccess<Advertiser> advertiserRepo;
        private readonly ILinearAccess<AdvertiserFavorites> favoriteAdvertiserRepo;
        private readonly ISessionService sessionService;

        public AdvertiserController(ILogger<AdvertiserController> logger, ISessionService sessionService, ILinearRepo repo)
        {
            this.logger = logger;
            this.sessionService = sessionService;
            this.advertiserRepo = repo.Advertiser;
            this.favoriteAdvertiserRepo = repo.FavoriteAdvertiser;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get(string id)
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
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
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var allAdvertisers = advertiserRepo.ReadAll();
            if (allAdvertisers == null) return StatusCode(404);

            var advertisers = allAdvertisers.Where((it) => it.AgencyId == user.AgencyId).ToList();

            var sortedAdvertisers = advertisers.OrderBy(adv => adv.Name);

            return Ok(sortedAdvertisers);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            if (!user.IsAdmin) return StatusCode(403);

            var data = advertiserRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }

        [HttpGet]
        [Route("favorites")]
        [Produces("application/json")]
        public IActionResult GetFavoritesByUser()
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var favorites = favoriteAdvertiserRepo.Read(user.Id);
            if (favorites == null || favorites.AdvertiserIds.Length < 1) return StatusCode(204);

            IList<Advertiser>? advertisers = advertiserRepo.ReadList(favorites.AdvertiserIds);
            if (advertisers == null) return StatusCode(500);
            if (advertisers.Count < 1) return StatusCode(204);

            var sortedAdvertisers = advertisers.OrderBy(adv => adv.Name);

            return Ok(sortedAdvertisers);
        }

        [HttpPost]
        [Route("favorites")]
        [Produces("application/json")]
        public IActionResult PostFavoritesByUser([FromBody] IList<Advertiser> data)
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var ids = data.Select(d => d.Id).ToList();
            if (ids == null) return StatusCode(500); // But may be empty to clear list :-)

            var validIds = advertiserRepo.ReadAll().Select((it)=> it.Id);
            foreach (var id in ids) {
                if (!validIds.Contains(id)) return StatusCode(400);
            }

            var newFavoritesObject = new AdvertiserFavorites(user.Id, ids.ToArray());

            favoriteAdvertiserRepo.Create(newFavoritesObject);

            return Ok(data);
        }
    }
}