using EFCoreDatabaseFirstLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IEmpDataAccess dal;
        public AccountController(IEmpDataAccess dal, IConfiguration config)
        {
            this.dal = dal;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(UserDetails user)
        {
            if (dal.Login(user))
            {
                //generate and return token
                var token = GetToken(user);
                return Ok(token);
            }
            else
            {
                return Unauthorized("invalid user");
            }
        }
        string GetToken(UserDetails user)
        {
            var key = _config["JWT:Key"];
            var byteKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));

            var descriptor = new JwtSecurityToken
                (
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: new SigningCredentials(byteKey, SecurityAlgorithms.HmacSha256),
                    claims: new List<Claim>
                    {
                        new Claim(ClaimTypes.Role,user.Role)
                    }
                );

            //generate token using descriptor 
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(descriptor);            
        }
    }
}
