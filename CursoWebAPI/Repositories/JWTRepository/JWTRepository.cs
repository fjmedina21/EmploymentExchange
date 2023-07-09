using AutoMapper;
using EmploymentExchange.Models;
using EmploymentExchange.Models.DTOs.Private;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using E = System.Text.Encoding;

namespace EmploymentExchange.Repositories
{
    public class JWTRepository : IJWT
    {
        private readonly IConfiguration configuration;
        private readonly IUser userRepo;
        private readonly IMapper mapper;

        public JWTRepository(IConfiguration configuration, IUser userRepo, IMapper mapper)
        {
            this.configuration = configuration;
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        public async Task<string> CreateJWTAsync(User user)
        {
            List<string> roles = new();
            User? entity = await userRepo.GetUserByIdAsync(user.Id);
            PrivateUserDTO PrivateUserDTO = mapper.Map<PrivateUserDTO>(entity);
            PrivateUserDTO.Roles.ForEach(r => roles.Add(r.Role.ToLower().Trim() ));

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role)); 
            
            SymmetricSecurityKey secretKey = new(E.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            SigningCredentials credentials = new(secretKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public async Task<List<string>> DecodeJWT(string token)
        //{
        //    string[] bearer = token.Split(" ");
        //    JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(bearer[1]);
        //    Guid guid = Guid.Parse(jwt.Claims.First(c => c.Type == "id").Value);
        //    string email = jwt.Claims.First(c => c.Type == "email").Value;
        
        //    List<string> roles = new();
        //    User? user = await userRepo.GetUserByIdAsync(guid);
        //    PGetUserDTO userDTO = mapper.Map<PGetUserDTO>(user);
        //    userDTO.Roles.ForEach(r => roles.Add(r.Role.ToLower().Trim() ));
            
        //    return roles;
        //}
    }
}
