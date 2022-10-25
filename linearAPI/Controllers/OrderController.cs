using linearAPI.Entities;
using linearAPI.Entities.BaseEntity;
using linearAPI.Repo;
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
        private readonly ILogger<OrderController> _logger;
        private readonly LinearRepo<LinearOrder> dataRepo = new LinearRepo<LinearOrder>("Generated/");
        private readonly SessionService sessionService = SessionService.GetRepo();

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        [Produces("application/json")]
        public IActionResult GetById(string id)
        {
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            LinearUser? user = sessionService.getUser(userName);
            if (user == null)
            {
                _logger.LogError($"Expected valid user with username, but {userName} not found by {nameof(SessionService)}.");
                return StatusCode(500);
            }

            var data = dataRepo.Read(id);

            if (data == null) return StatusCode(404);

            if (user.IsAdmin) return Ok(data);

            if (data.HandlerId != user.Id) return StatusCode(403);

            return Ok(data);
        }

        [HttpGet]
        [Route("mine")]
        [Produces("application/json")]
        public IActionResult GetByUser(string userId)
        {
            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            LinearUser? callingUser = sessionService.getUser(userName);
            if (callingUser == null)
            {
                _logger.LogError($"Expected valid user with username, but {userName} not found by {nameof(SessionService)}.");
                return StatusCode(500);
            }

            var data = dataRepo.ReadAll();
            if (data == null) return StatusCode(404);

            // Block impersonation for non-admins
            if (callingUser.IsAdmin == false && userId != callingUser.Id) return StatusCode(403);

            var userData = data.Where((it) => it.HandlerId == userId);

            return Ok(userData);
        }

        [HttpGet]
        [Route("all")]
        [Produces("application/json")]
        public IActionResult Get()
        {

            string? userName = HttpContext.User.Claims.FirstOrDefault()?.Value;
            if (userName == null) return StatusCode(401);

            LinearUser? handler = sessionService.getUser(userName);
            if (handler == null)
            {
                _logger.LogError($"Expected valid user with username, but {userName} not found by {nameof(SessionService)}.");
                return StatusCode(500);
            }

            if (!handler.IsAdmin) return StatusCode(403);

            var data = dataRepo.ReadAll();
            if (data == null) return StatusCode(404);

            if (handler.IsAdmin) return Ok(data);

            return Ok(data.Take(100));
        }
    }
}