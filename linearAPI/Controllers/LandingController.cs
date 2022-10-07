
using linearAPI.Entities;
using linearAPI.Repo;
using linearAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace linearAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class LandingController : ControllerBase
    {
        private readonly ILogger<LandingController> _logger;
        private LinearRepo<LinearData> dataRepo = new LinearRepo<LinearData>();
        private SessionService sessionRepo = SessionService.GetRepo();

        public LandingController(ILogger<LandingController> logger)
        {
            _logger = logger;
            //debug
            dataRepo.Create(new LinearData("123", "This is a piece of data", DateTime.Now));
            dataRepo.Create(new LinearData("124", "This is more data", DateTime.Now.AddSeconds(1)));
            dataRepo.Create(new LinearData("125", "More is better", DateTime.Now.AddSeconds(2)));
        }

        [HttpGet(Name = "GetLanding")]
        public IActionResult Get()
        {
            //// Get user id (example, not used here)
            //var claims = HttpContext.User.Claims;
            //var id = claims.FirstOrDefault().Value;

            var data = dataRepo.ReadAll();
            if (data == null) return StatusCode(301);

            return Ok(data);
        }

        [Serializable]
        public class LinearData : Database.Entities.ILinearEntity
        {
            public string Id { get; set; }
            public DateTime ModifiedTime { get; set; }
            public DateTime CreatedTime { get; set; }

            public LinearData(string id, string message, DateTime date)
            {
                // Meta (inherited)
                Id = id;
                ModifiedTime = DateTime.Now;
                CreatedTime = DateTime.Now;

                // Values
                Message = message;
                Date = date;
            }

            public string Message { get; }
            public DateTime Date { get; }
        }
    }
}