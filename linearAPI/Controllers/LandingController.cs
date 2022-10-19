using linearAPI.Entities;
using linearAPI.Entities.BaseEntity;
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
        private LinearRepo<LinearOrder> dataRepo = new LinearRepo<LinearOrder>("Generated/");
        private SessionService sessionRepo = SessionService.GetRepo();

        public LandingController(ILogger<LandingController> logger)
        {
            _logger = logger;
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

        //[Serializable]
        //public class LinearData : ILinearEntity
        //{
        //    public string Id { get; set; }
        //    public DateTime ModifiedTime { get; set; }
        //    public DateTime CreatedTime { get; set; }

        //    public LinearData(string id, string message, DateTime date)
        //    {
        //        // Meta (inherited)
        //        Id = id;
        //        ModifiedTime = DateTime.Now;
        //        CreatedTime = DateTime.Now;

        //        // Values
        //        Message = message;
        //        Date = date;
        //    }

        //    public string Message { get; }
        //    public DateTime Date { get; }
        //}
    }
}