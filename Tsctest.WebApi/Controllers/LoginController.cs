using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tsctest.WebApi.Model;

namespace Tsctest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Login process 
        /// </summary>
        /// <param name="user">receives a user-type parameter</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public IActionResult Login([FromBody] UserModel user)
        {
            //TODO: this should be async
            var _userInfo = AuthenticateUser(user.User, user.Password);
            if (_userInfo != null)
            {
                return Ok(new { token = GenerarTokenJWT(_userInfo) });
            }
            else
            {
                return Unauthorized();
            }
        }


        /// <summary>
        /// Internal method to Authenticate the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private User AuthenticateUser(string user, string password)
        {
            //TODO: this should be a REAL db request to find the user
            //TODO: this should be async


            //Dummy user from the appsettings.json
            var dummyUser = _configuration["DummyLogin:User"];
            var dummyPassword = _configuration["DummyLogin:Password"];

            if (user == dummyUser && dummyPassword == password)
            {
                return new User()
                {
                    Id = 1,
                    FirstName = "David",
                    LastName = "Mansilla",
                    Email = "rdavidmansilla@gmail.com",
                    Role = "Administrator"
                };
            }

            return null;
        }

        /// <summary>
        /// Internal method to create the session JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerarTokenJWT(User user)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("firstname", user.FirstName),
                new Claim("lastname", user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var _Payload = new JwtPayload(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(30) //Just alive for 30 minutes
                );

            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

        /// <summary>
        /// Check if the provided token still valid
        /// </summary>
        /// <param name="token">String token (stored in session)</param>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public bool ValidateCurrentToken([FromBody] TokenModel token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]));

            var myIssuer = _configuration["JWT:Issuer"];
            var myAudience = _configuration["JWT:Audience"];

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
