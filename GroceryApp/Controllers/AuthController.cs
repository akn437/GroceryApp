using GroceryApp.Models;
using JwtAuthentication.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GroceryAppContext _dbContext;

        public AuthController(GroceryAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            Console.WriteLine(user.EmailId+user.Password);

            if (user is null)
            {
                return BadRequest("Invalid client request");
            }

            var dbUser = _dbContext.Users.SingleOrDefault(u => u.EmailId == user.EmailId && u.Password == user.Password);

            if (dbUser!=null )
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims= new List<Claim>();
                if (dbUser.EmailId == "admin@admin.com")
                {
                    claims = new List<Claim>
                    {
                      new Claim(ClaimTypes.Name, user.EmailId),
                      new Claim(ClaimTypes.Role, "Admin")
                    };
                }
                else
                {
                    claims = new List<Claim>
                    {
                      new Claim(ClaimTypes.Name, user.EmailId),
                      new Claim(ClaimTypes.Role, "User")
                    };
                }
                

                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://localhost:5001",
                    audience: "http://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(new AuthenticatedResponse { Token = tokenString, UserName=dbUser.FirstName, EmailId=dbUser.EmailId, Phone=dbUser.MobileNo });


            }

            return Unauthorized();
        }


        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_dbContext.Users== null)
            {
                return BadRequest();
            }
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok();
        }
    }



}

