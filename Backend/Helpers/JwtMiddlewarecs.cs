using Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachToContext(context, token);
            }

            await _next(context);
        }

        private void AttachToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                if (jwtToken.Claims.FirstOrDefault(x => x.Type == "userId") != null)
                {
                    var userId = long.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);
                    context.Items["userId"] = userId;
                }
                else if (jwtToken.Claims.FirstOrDefault(x => x.Type == "delivererId") != null)
                {
                    var delivererId = long.Parse(jwtToken.Claims.First(x => x.Type == "delivererId").Value);
                    context.Items["delivererId"] = delivererId;
                }
                else if (jwtToken.Claims.FirstOrDefault(x => x.Type == "stationId") != null)
                {
                    var stationId = long.Parse(jwtToken.Claims.First(x => x.Type == "stationId").Value);
                    context.Items["stationId"] = stationId;
                }
                else if (jwtToken.Claims.FirstOrDefault(x => x.Type == "ownerId") != null)
                {
                    var ownerId = long.Parse(jwtToken.Claims.First(x => x.Type == "ownerId").Value);
                    context.Items["ownerId"] = ownerId;
                }
            }
            catch
            {

            }
        }
    }
}