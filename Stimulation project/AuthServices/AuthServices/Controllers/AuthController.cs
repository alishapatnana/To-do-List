using AuthServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        private readonly AppDbContext _context;
        private IConfiguration _config;

        public AuthController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        //[HttpPost("UserModel")]
        //public UserModel GetUser(UserModel valuser)
        //{
        //    var user = _context.UserList.FirstOrDefault(c => c.UserName == valuser.UserName && c.Password == valuser.Password);

        //    if (user == null)
        //    {

        //        return null;
        //    }

        //    return user;
        //}
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserModel login)
        {

            _log4net.Info("Authentication initiated");
            IActionResult response = Unauthorized();
            var user = _context.UserList.FirstOrDefault(c => c.UserName == login.UserName && c.Password == login.Password);
            if (user == null)
            {
                _log4net.Info("Error");
                return NotFound();
            }
            else
            {
                _log4net.Info("Login credential matched");
                return Ok(new
                {
                    token = GenerateJSONWebToken(user)
                });
            }
        }
        private string GenerateJSONWebToken(UserModel userInfo)
        {
            _log4net.Info("Token Generation initiated");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
