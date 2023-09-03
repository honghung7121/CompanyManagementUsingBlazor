using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Dto;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        [HttpPost] 
        public IActionResult Login(Auth info) 
        {
            if(info.UserName == "Admin" && info.Password == "Admin")
            {
                string token = GenerateToken(info);
                return Ok(token);
            }
            return BadRequest("Invalid username or password!");
        }
        private string GenerateToken(Auth info)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, info.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
