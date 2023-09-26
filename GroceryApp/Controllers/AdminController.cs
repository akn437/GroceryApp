using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JwtAuthentication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet, Authorize(Roles = "Admin,User")]
        public IActionResult Get()
        {
            Console.WriteLine(User.Identity.Name);
            return Ok();
        }
    }
}
