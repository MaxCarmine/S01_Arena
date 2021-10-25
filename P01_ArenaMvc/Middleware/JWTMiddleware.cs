using Lucene.Net.Support;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using P01_ArenaMvc.JWT;
using ServiceStack.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace P01_ArenaMvc.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
       

        public JWTMiddleware(RequestDelegate next) {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context) {

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = ValidateToken(token);
            if (userId != null) {
                context.Items["username"] = "username";
            }
            await _next(context);
        }

        public string? ValidateToken(string token) {
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
                var username = jwtToken.Claims.First(x => x.Type == "username").Value;
                return username;
            } catch (InvalidOperationException) {
                return null;
            }

        }
    }
}
    

