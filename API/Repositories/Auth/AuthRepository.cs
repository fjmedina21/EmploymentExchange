using AutoMapper;
using DispensarioMedico.Helpers;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AuthRepository : IAuth
    {
        private PasswordHashing hashing = new();
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

            if (user is null || !hashing.ComparePassword(login.Password, user.Password)) return (null, null);

            string token = await jwt.CreateJWTAsync(user);
            GetUserDTO userDTO = mapper.Map<GetUserDTO>(user);

            return (userDTO, token);
        }

        public Task<GetUserDTO> SignupAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse> ChangePasswordAsync(ChangePasswordDTO model, string token)
        {
            (Guid userId, _) = jwt.DecodeJWT(token);

            User? user = await dbContext.Users.Where(e => e.State)
                .FirstOrDefaultAsync(e => e.Id.Equals(userId));

            if (user is null) return new APIResponse(StatusCode: 404);

            bool currentPasswordMatch = hashing.ComparePassword(model.CurrentPassword, user.Password);
            bool newPasswordMatch = hashing.ComparePassword(model.NewPassword, user.Password);

            if (!currentPasswordMatch) return new APIResponse(StatusCode: 400, Message: "Incorrect current password ");
            if (newPasswordMatch) return new APIResponse(StatusCode: 400, Message: "New password can't be the same");

            user.Password = hashing.HashPassword(model.NewPassword);
            await dbContext.SaveChangesAsync();

            return new APIResponse();
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
