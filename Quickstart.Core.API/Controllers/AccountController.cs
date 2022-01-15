using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Quickstart.Core.BL.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Quickstart.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public AccountController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                });

            if (loginDTO.Password == "123456")
            {
                var userDTO = new UserDTO
                {
                    Id = 1,
                    FirstMidName = "David",
                    LastName = "Santafe"
                };

                var token = GenerateTokenJwt(userDTO);
                //var token = GenerateTokenJwt2(userDTO);

                return Ok(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.OK,
                    Data = new { token }
                });
            }

            return Ok(new ResponseDTO
            {
                Code = (int)HttpStatusCode.Unauthorized
            });
        }

        private string GenerateTokenJwt(UserDTO userDTO)
        {
            // appsetting for Token JWT
            var secretKey = configuration["JWT:SECRET_KEY"];
            var audienceToken = configuration["JWT:AUDIENCE_TOKEN"];
            var issuerToken = configuration["JWT:ISSUER_TOKEN"];
            var expireTime = configuration["JWT:EXPIRE_MINUTES"];

            //CREAMOS EL HEADER 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //CREAMOS LOS CLAIMS
            var claimsIdentity = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userDTO.Id.ToString()),
                new Claim(ClaimTypes.Name, userDTO.FullName)
            };

            //CREAMOS EL PAYLOAD
            var payload = new JwtPayload(issuer: issuerToken,
                    audience: audienceToken,
                    claims: claimsIdentity,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)));

            //GENERAMOS EL TOKEN
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateTokenJwt2(UserDTO userDTO)
        {
            // appsetting for Token JWT
            var secretKey = configuration["JWT:SECRET_KEY"];
            var audienceToken = configuration["JWT:AUDIENCE_TOKEN"];
            var issuerToken = configuration["JWT:ISSUER_TOKEN"];
            var expireTime = configuration["JWT:EXPIRE_MINUTES"];

            //CREAMOS EL HEADER 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userDTO.FullName) });

            // create token to the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}
