﻿using AutoMapper;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using EmploymentExchangeAPI.Models;
using EmploymentExchangeAPI.Models.Private;

namespace EmploymentExchangeAPI.Repositories
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
            PrivateUserDTO.Roles.ForEach(r => roles.Add(r.Role.ToLower().Trim()));

            List<Claim> claims = new()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email)
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

        public object DecodeJWT(string token)
        {
            JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            Guid Id = Guid.Parse(jwt.Claims.First(c => c.Type == "id").Value);
            string Email = jwt.Claims.First(c => c.Type == "email").Value;
            List<Claim>? RolesClaim = jwt.Claims.Where(c => c.Type == "roles").ToList();
            
            List<string> Roles = new();
            foreach (var role in RolesClaim) Roles.Add(role.Value);
            object jwtProp = new { Id, Email, Roles };

            return  jwtProp;
        }
    }
}
