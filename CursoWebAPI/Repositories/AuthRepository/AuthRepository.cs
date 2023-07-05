using AutoMapper;
using EmploymentExchange.Data;
using EmploymentExchange.Models;
using EmploymentExchange.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmploymentExchange.Repositories
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

        public async Task<LoggedInDTO?> LogInAsync(LoginDTO login)
        {
            User? user = await dbContext.Users.AsNoTracking().Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Email == login.Email);

            if (user == null || !user.ComparePassword(login.Password)) return null;

            LoggedInDTO loggedin = mapper.Map<LoggedInDTO>(user);
            loggedin.token = jwt.CreateJWT(user);

            return loggedin;
        }

        public Task<LoggedInDTO> SignUpAsync()
        {
            throw new NotImplementedException();
        }
    }
}
