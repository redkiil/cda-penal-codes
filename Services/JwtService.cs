using CDA.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CDA.Services
{
    public static class JwtService
    {
        public static string GenerateToken(User user)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var authkey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Key"];
            Console.WriteLine(authkey);
            var key = Encoding.ASCII.GetBytes(authkey);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(token);


        }
    }
}
