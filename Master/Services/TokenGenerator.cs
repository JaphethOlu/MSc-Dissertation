using System;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using Master.Models;
using Master.Interfaces.Models;
using Master.Interfaces.Services;

namespace Master.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration config;

        public TokenGenerator(IConfiguration config)
        {
            this.config = config;
        }

        public string GenerateToken(IUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Email, user.EmailAddress),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey userKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            SigningCredentials userCredentials = new SigningCredentials(userKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken userToken = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: userCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(userToken);
        }
    }
}