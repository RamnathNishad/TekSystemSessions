using EFCoreDatabaseFirstLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
            var byteKey=System.Text.Encoding.UTF8.GetBytes(key);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer= _config["JWT:Audience"],
                Audience= _config["JWT:Issuer"],
                Expires= DateTime.UtcNow.AddMinutes(10),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(byteKey),SecurityAlgorithms.HmacSha256Signature)
            };

            //generate token using descriptor 
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);            
        }
    }
}
