using Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class UserLoginSessionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public UserLoginSessionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AppSettings.SECRET_KEY);
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken != null)
                {
                    var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                    // attach account to context on successful jwt validation
                    context.Items["Id"] = accountId;
                }
               
            }
            catch (Exception)
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
                //await attachAccountToContext(context,token, userRepository, userService);

            await _next(context);
        }
    }
}
