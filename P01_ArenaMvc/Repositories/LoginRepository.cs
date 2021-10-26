using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using P01_ArenaMvc.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Repositories
{
    public class LoginRepository
    {

        public LoginRepository() {
            
            
        }

        public bool UserExists(string username) {
            if (username == "user") {
                return true;
            }
            return false;
        }

        public string GetToken(string username) {
            return GenerateToken(username);
        }

        public string GenerateToken(string username) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("DA1FCB34143D10F8FFFDEE810D22F5A5C7683EE34BBCF63B9404FA25D1D63D8E");
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] { new Claim("username", username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
