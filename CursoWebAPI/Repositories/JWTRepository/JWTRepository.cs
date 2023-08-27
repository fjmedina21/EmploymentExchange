using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using EmploymentExchangeAPI.Models;

namespace EmploymentExchangeAPI.Repositories
{
    public class JWTRepository : IJWT
    {
        private readonly IConfiguration configuration;
        private readonly IUser userRepo;

        public JWTRepository(IConfiguration configuration, IUser userRepo)
        {
            this.configuration = configuration;
            this.userRepo = userRepo;
        }

        public async Task<string> CreateJWTAsync(User user)
        {
            List<string> roles = new();
            User? entity = await userRepo.GetUserByIdAsync(user.Id);
            entity?.Roles.ToList().ForEach(role => roles.Add(role.Name.ToLower().Trim()));

            List<Claim> claims = new()
            {
                new Claim("id", user.Id.ToString()),
            };

            foreach (var role in roles) claims.Add(new Claim("roles", role));

            SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            SigningCredentials credentials = new(secretKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (Guid, List<string>) DecodeJWT(string token)
        {
            string[] bearerToken = token.Split(' ');
            JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(bearerToken[1]);

            Guid Id = Guid.Parse(jwt.Claims.First(c => c.Type == "id").Value);
            List<Claim>? RolesClaim = jwt.Claims.Where(c => c.Type == "roles").ToList();
            
            List<string> Roles = new();
            foreach (var role in RolesClaim) Roles.Add(role.Value);

            return (Id, Roles);
        }
    }
}
