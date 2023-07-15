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

        public async Task<(GetUserDTO?, string?)> LoginAsync(LoginDTO login)
        {
            User? user = await dbContext.Users.AsNoTracking().Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Email == login.Email);

            if (user is null || !user.ComparePassword(login.Password)) return (null, null);

            string token = await jwt.CreateJWTAsync(user);
            GetUserDTO userDTO = mapper.Map<GetUserDTO>(user);

            return (userDTO, token);
        }

        public Task<GetUserDTO> SignupAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> ChangePasswordAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> ForgotPasswordAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> ResetPasswordAsync()
        {
            throw new NotImplementedException();
        }
    }
}
