using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace P01_ArenaMvc.Middleware
{
    public class JWTMiddleware : IMiddleware
    {
      
        public JWTMiddleware() {
            
            
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token)) {
                Console.WriteLine("The toekn is not null ");
                attachUserToContext(context, token);
                await next(context);
            }
        }

        private void attachUserToContext(HttpContext context, string token) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("DA1FCB34143D10F8FFFDEE810D22F5A5C7683EE34BBCF63B9404FA25D1D63D8E");
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    //ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "username");
                context.Items["username"] = "user";
            } catch {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
    

