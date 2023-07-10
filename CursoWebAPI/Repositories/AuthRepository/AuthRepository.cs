using AutoMapper;
using EmploymentExchangeAPI.Data;
using EmploymentExchangeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchangeAPI.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly MyDBContext dbContext;
        private readonly IMapper mapper;
        private readonly IJWT jwt;

        public AuthRepository(MyDBContext dbContext, IMapper mapper, IJWT jwt)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.jwt = jwt;
        }

        public async Task<(GetUserDTO?, string?)> LogInAsync(LoginDTO login)
        {
            User? user = await dbContext.Users.AsNoTracking().Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Email == login.Email);

            if (user == null || !user.ComparePassword(login.Password)) return (null, null);

            string token = await jwt.CreateJWTAsync(user);
            GetUserDTO userDTO = mapper.Map<GetUserDTO>(user);

            return (userDTO, token);
        }

        public Task<GetUserDTO> SignUpAsync()
        {
            throw new NotImplementedException();
        }
    }
}
