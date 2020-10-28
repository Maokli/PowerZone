using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(_config["Token:Key"]));
        }

    public string CreateToken(Client client)
    {
      var claims = new List<Claim>{
          new Claim(JwtRegisteredClaimNames.Email,client.Email),
          new Claim(JwtRegisteredClaimNames.GivenName, client.UserName),
          new Claim(ClaimTypes.Role, client.Role)
      };
      
      var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
    
      var tokenDescriptor = new SecurityTokenDescriptor{
          Subject = new ClaimsIdentity(claims),
          Expires = DateTime.UtcNow.AddDays(7),
          SigningCredentials = creds,
          Issuer = _config["Token:Issuer"]
      };

      var tokenHandler = new JwtSecurityTokenHandler();


      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}