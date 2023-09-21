using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet, Authorize(Roles = "User")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Admin", "Admin" };
        }
    }
}
