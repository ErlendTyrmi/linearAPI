
using Common.Interfaces;
using LinearAPI.Services;
using LinearEntities.Entities;
using LinearMockDatabase.Repo.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;


namespace Entities.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly ILinearAccess<LinearOrder> OrderRepo;
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
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = OrderRepo.Read(id);

            if (data == null) return StatusCode(404); // No content is not an error

            if (user.IsAdmin) return Ok(data);

            if (data.HandlerId != user.Id) return StatusCode(403);

            return Ok(data);
        }

        [HttpGet]
        [Route("own")]
        [Produces("application/json")]
        public IActionResult GetByUserId()
        {
            var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = OrderRepo.ReadAll();
            if (data == null) return StatusCode(404);
            if (data.Count < 1) return StatusCode(204); // Not found


            var orders = data.Where((it) => it.HandlerId == user.Id || user.IsAdmin == true);

            var sortedOrders = orders.OrderBy(adv => adv.StartDate);

            return Ok(sortedOrders);
        }
    }
}