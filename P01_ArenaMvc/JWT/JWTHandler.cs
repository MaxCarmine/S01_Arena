using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace P01_ArenaMvc.JWT
{
    
    public  interface IJWTHandler
    {
        public string GenerateToken(string username);
        public int? ValidateToken(string token);
    }
    public class JWTHandler : IJWTHandler
    {
        private readonly MyScreteKey _appSettings;

        public JWTHandler(IOptions<MyScreteKey> appSettings) {
            _appSettings = appSettings.Value;
        }
        public string GenerateToken(string username) {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[] { new Claim("username", username) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int? ValidateToken(string token) {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("DA1FCB34143D10F8FFFDEE810D22F5A5C7683EE34BBCF63B9404FA25D1D63D8E");
            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = int.Parse(jwtToken.Claims.First(x => x.Type == "username").Value);

                // return user id from JWT token if validation successful
                return username;
            } catch(InvalidOperationException) {
                // return null if validation fails
                return 2;
            }
        }
    }
}
