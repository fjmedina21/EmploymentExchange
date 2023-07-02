using EmploymentExchange.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Encoding = System.Text.Encoding;

namespace EmploymentExchange.Repositories
{
    public class JWTRepository : IJWT
    {
        private readonly IConfiguration configuration;

        public JWTRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJWT(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));

            SigningCredentials credentials = new(secretKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ValidateJWT(string token)
        {
            throw new NotImplementedException();
        }
    }
}
