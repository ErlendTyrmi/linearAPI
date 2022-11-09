
using LinearAPI.Services;
using LinearEntities.Entities;
using LinearMockDatabase;
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
           var user = sessionService.AssertSignedIn(HttpContext.User.Claims.FirstOrDefault()?.Value);
            if (user == null) return StatusCode(401);

            var data = salesProductRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}