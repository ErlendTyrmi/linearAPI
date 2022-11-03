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
    public class SalesProductController : ControllerBase
    {
        private readonly ILogger<SalesProductController> logger;
        private readonly LinearAccess<LinearSalesProduct> salesProductRepo;
        private readonly ISessionService sessionService;

        public SalesProductController(ILogger<SalesProductController> logger, ILinearRepo repo, ISessionService sessionService)
        {
            this.logger = logger;
            this.salesProductRepo = repo.SalesProduct;
            this.sessionService = sessionService;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault()?.Value;
            var user = sessionService.AssertSignedIn(userId);
            if (user == null) return StatusCode(401);

            var data = salesProductRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}