using API.IHelpers;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Helpers
{
    public class JWTManager : IJWTManager
    {
        private readonly IConfiguration config;

        public JWTManager(IConfiguration config)
        {
            this.config = config;
        }

        //public string GenerateToken(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim("id", user.Id.ToString()),
        //        new Claim("name", user.Name),
        //        new Claim(ClaimTypes.Role, user.Role.ToString()),
        //        new Claim("isFirstLogin", user.IsFirstLogin.ToString())
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8
        //        .GetBytes(config.GetSection("AppSettings:Token").Value));

        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.Now.AddDays(1),
        //        SigningCredentials = creds
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    return tokenHandler.WriteToken(token);
        //}
    }
}
