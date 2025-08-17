using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TaskApi
{
    public static class JwtTokenGenerator
    {
        public static string GenerateTestToken(IConfiguration config)
        {
            var secret = config.GetValue<string>("AppSettings:Token");
            var key = new SymmetricSecurityKey(Convert.FromBase64String(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "testUser"),
                new Claim(ClaimTypes.Role, "Admin") // можеш да смениш на User ако искаш
            };

            var token = new JwtSecurityToken(
                issuer: config["AppSettings:Issuer"],
                audience: config["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

