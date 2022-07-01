using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIService.Domain.Entities;

namespace WebAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult Post([FromBody] UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The Model is not valid");
            }
            if (user.UserName != "Raheleh" || user.Password != "123")
            {
                return Unauthorized();
            }
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var tockenOption = new JwtSecurityToken(
                issuer: "http://localhost:18658",
                claims: new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tockenOption);
            return Ok(new { token = tokenString });
        }

    }
}
