using linearAPI.Entities;
using linearAPI.Entities.BaseEntity;
using linearAPI.Repo;
using linearAPI.Repo.Database;
using linearAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;


namespace linearAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly LinearAccess<LinearOrder> OrderRepo;
        private readonly ISessionService sessionService;

        public OrderController(ILogger<OrderController> logger, ISessionService sessionService, ILinearRepo repo)
        {
            this.logger = logger;
            this.sessionService = sessionService;
            this.OrderRepo = repo.Order;
        }

        [HttpGet]
        [Route("/")]
        [Produces("application/json")]
        public IActionResult GetById(string id)
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = OrderRepo.Read(id);

            if (data == null) return StatusCode(404);

            if (user.IsAdmin) return Ok(data);

            if (data.HandlerId != user.Id) return StatusCode(403);

            return Ok(data);
        }

        [HttpGet]
        [Route("own")]
        [Produces("application/json")]
        public IActionResult GetByUserId()
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = OrderRepo.ReadAll();
            if (data == null) return StatusCode(404);

            var userData = data.Where((it) => it.HandlerId == user.Id);

            return Ok(userData);
        }

        //[HttpGet]
        //[Route("all")]
        //[Produces("application/json")]
        //public IActionResult Get()
        //{

        //    string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
        //    if (userName == null) return StatusCode(401);

        //    LinearUser? handler = sessionService.getUser(userName);
        //    if (handler == null)
        //    {
        //        _logger.LogError($"Expected valid user with username, but {userName} not found by {nameof(SessionService)}.");
        //        return StatusCode(500);
        //    }

        //    if (!handler.IsAdmin) return StatusCode(403);

        //    var data = dataRepo.ReadAll();
        //    if (data == null) return StatusCode(404);

        //    if (handler.IsAdmin) return Ok(data);

        //    return Ok(data.Take(100));
        //}
    }
}