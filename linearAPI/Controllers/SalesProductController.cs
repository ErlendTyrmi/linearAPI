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
    public class SalesProductController : ControllerBase
    {
        private readonly ILogger<SalesProductController> _logger;
        private LinearRepo<LinearSalesProduct> dataRepo = new LinearRepo<LinearSalesProduct>("Generated/");

        public SalesProductController(ILogger<SalesProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var data = dataRepo.ReadAll();
            if (data == null) return StatusCode(404);

            return Ok(data);
        }
    }
}